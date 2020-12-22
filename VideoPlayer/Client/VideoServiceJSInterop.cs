using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorWasmVideo.Client
{
    public class VideoServiceJSInterop : IAsyncDisposable
    {
        private IJSRuntime _jsRuntime;
        private IJSObjectReference _reference;

        public VideoServiceJSInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitAsync()
        {
            _reference = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./videoServiceJSInterop.js");
        }

        public async Task<string> CreateURLFromBuffer(byte[] buffer, string name, string type)
        {
            var data = Convert.ToBase64String(buffer);

            return await _reference.InvokeAsync<string>("createObjectURL", data, name, type);
        }

        public async ValueTask DisposeAsync()
        {
            if (_reference != null)
            {
                await _reference.InvokeVoidAsync("dispose");
                await _reference.DisposeAsync();
            }
        }
    }
}
