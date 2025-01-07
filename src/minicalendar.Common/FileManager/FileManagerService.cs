using Microsoft.JSInterop;

namespace minicalendar.Common.FileManager;

public class FileManagerService(IJSRuntime jsRuntime)
{
    private IJSObjectReference? _module;
    
    public async Task SaveAsAsync(string filename, byte[] data)
    {
        await ImportModuleAsync();

        if (_module == null)
        {
            return;
        }
        await _module.InvokeAsync<object>(
            "common_fileManager_saveAsFile",
            filename,
            Convert.ToBase64String(data));
    }

    public async Task ImportModuleAsync()
    {
        _module ??= await jsRuntime.InvokeAsync<IJSObjectReference>("import",
            "./_content/minicalendar.Common/FileManager/FileManager.razor.js");
    }
}