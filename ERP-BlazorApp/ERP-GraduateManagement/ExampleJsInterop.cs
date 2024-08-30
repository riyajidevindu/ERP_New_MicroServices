// ExampleJsInterop.cs
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace ERP_GraduateManagement
{
    public class ExampleJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ExampleJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/ERP_GraduateManagement/exampleJsInterop.js").AsTask());
        }

        public async ValueTask<string> Prompt(string message)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("showPrompt", message);
        }

        public async ValueTask InvokeSaveAsFile(string fileName, string byteBase64)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("saveAsFile", fileName, byteBase64);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
