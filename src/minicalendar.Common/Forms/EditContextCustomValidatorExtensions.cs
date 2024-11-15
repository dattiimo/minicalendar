﻿using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components.Forms;

namespace minicalendar.Common.Forms;

/// <summary>
/// Extension methods to add DataAnnotations validation to an <see cref="EditContext"/>.
/// </summary>
public static class EditContextCustomDataAnnotationsExtensions
{
    /// <summary>
    /// Adds CustomDataAnnotations validation support to the <see cref="EditContext"/>.
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/>.</param>
    [Obsolete("Use " + nameof(EnableCustomDataAnnotationsValidation) + " instead.")]
    public static EditContext AddCustomDataAnnotationsValidation(this EditContext editContext)
    {
        EnableCustomDataAnnotationsValidation(editContext);
        return editContext;
    }

    /// <summary>
    /// Enables CustomDataAnnotations validation support for the <see cref="EditContext"/>.
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/>.</param>
    /// <returns>A disposable object whose disposal will remove CustomDataAnnotations validation support from the <see cref="EditContext"/>.</returns>
    [Obsolete("This API is obsolete and may be removed in future versions. Use the overload that accepts an IServiceProvider instead.")]
    public static IDisposable EnableCustomDataAnnotationsValidation(this EditContext editContext)
    {
        return new CustomDataAnnotationsEventSubscriptions(editContext, null!);
    }
    /// <summary>
    /// Enables CustomDataAnnotations validation support for the <see cref="EditContext"/>.
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/>.</param>
    /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to be used in the <see cref="ValidationContext"/>.</param>
    /// <returns>A disposable object whose disposal will remove CustomDataAnnotations validation support from the <see cref="EditContext"/>.</returns>
    public static IDisposable EnableCustomDataAnnotationsValidation(this EditContext editContext, IServiceProvider serviceProvider)
    {
        if (serviceProvider == null)
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }
        return new CustomDataAnnotationsEventSubscriptions(editContext, serviceProvider);
    }

    private static event Action? OnClearCache;

    private static void ClearCache(Type[]? _)
    {
        OnClearCache?.Invoke();
    }

    private sealed class CustomDataAnnotationsEventSubscriptions : IDisposable
    {
        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo?> _propertyInfoCache = new();

        private readonly EditContext _editContext;
        private readonly IServiceProvider? _serviceProvider;
        private readonly ValidationMessageStore _messages;

        public CustomDataAnnotationsEventSubscriptions(EditContext editContext, IServiceProvider serviceProvider)
        {
            _editContext = editContext ?? throw new ArgumentNullException(nameof(editContext));
            _serviceProvider = serviceProvider;
            _messages = new ValidationMessageStore(_editContext);

            //_editContext.OnFieldChanged += OnFieldChanged;
            _editContext.OnValidationRequested += OnValidationRequested;

            if (MetadataUpdater.IsSupported)
            {
                OnClearCache += ClearCache;
            }
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "Model types are expected to be defined in assemblies that do not get trimmed.")]
        private void OnFieldChanged(object? sender, FieldChangedEventArgs eventArgs)
        {
            var fieldIdentifier = eventArgs.FieldIdentifier;
            if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
            {
                var propertyValue = propertyInfo.GetValue(fieldIdentifier.Model);
                var validationContext = new ValidationContext(fieldIdentifier.Model, _serviceProvider, items: null)
                {
                    MemberName = propertyInfo.Name
                };
                var results = new List<ValidationResult>();

                Validator.TryValidateProperty(propertyValue, validationContext, results);
                _messages.Clear(fieldIdentifier);
                foreach (var result in CollectionsMarshal.AsSpan(results))
                {
                    _messages.Add(fieldIdentifier, result.ErrorMessage!);
                }

                // We have to notify even if there were no messages before and are still no messages now,
                // because the "state" that changed might be the completion of some async validation task
                _editContext.NotifyValidationStateChanged();
            }
        }

        [UnconditionalSuppressMessage("Trimming", "IL2026", Justification = "Model types are expected to be defined in assemblies that do not get trimmed.")]
        private void OnValidationRequested(object? sender, ValidationRequestedEventArgs e)
        {
            var validationContext = new ValidationContext(_editContext.Model, _serviceProvider, items: null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_editContext.Model, validationContext, validationResults, true);

            // Transfer results to the ValidationMessageStore
            _messages.Clear();
            foreach (var validationResult in validationResults)
            {
                if (validationResult == null)
                {
                    continue;
                }

                var hasMemberNames = false;
                foreach (var memberName in validationResult.MemberNames)
                {
                    hasMemberNames = true;
                    _messages.Add(_editContext.Field(memberName), validationResult.ErrorMessage!);
                }

                if (!hasMemberNames)
                {
                    _messages.Add(new FieldIdentifier(_editContext.Model, fieldName: string.Empty), validationResult.ErrorMessage!);
                }
            }

            _editContext.NotifyValidationStateChanged();
        }

        public void Dispose()
        {
            _messages.Clear();
            _editContext.OnFieldChanged -= OnFieldChanged;
            _editContext.OnValidationRequested -= OnValidationRequested;
            _editContext.NotifyValidationStateChanged();

            if (MetadataUpdater.IsSupported)
            {
                OnClearCache -= ClearCache;
            }
        }

        [UnconditionalSuppressMessage("Trimming", "IL2080", Justification = "Model types are expected to be defined in assemblies that do not get trimmed.")]
        private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
        {
            var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
            if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
            {
                // CustomDataAnnotations only validates public properties, so that's all we'll look for
                // If we can't find it, cache 'null' so we don't have to try again next time
                propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                // No need to lock, because it doesn't matter if we write the same value twice
                _propertyInfoCache[cacheKey] = propertyInfo;
            }

            return propertyInfo != null;
        }

        internal void ClearCache()
        {
            _propertyInfoCache.Clear();
        }
    }
}
