using Microsoft.AspNetCore.Components.Forms;

namespace minicalendar.Common.Forms.Validation;

public class TwValidationCssClassProvider : FieldCssClassProvider
{
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        var isInvalid = editContext.GetValidationMessages(fieldIdentifier).Any();
        return isInvalid ? " border-red-800" : string.Empty;
    }
}