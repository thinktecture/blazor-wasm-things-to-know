using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorWasmVideo.Client
{
    public class VideoService : IAsyncDisposable
    {
        private IJSUnmarshalledObjectReference _reference;

        public async Task InitAsync(IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("import", "./video.js");

            var unmarshalledRuntime = (IJSUnmarshalledRuntime)jsRuntime;
            _reference = unmarshalledRuntime.InvokeUnmarshalled<IJSUnmarshalledObjectReference>("videoServiceReference");
        }

        public string CreateURLFromBuffer(byte[] buffer, string name, string type)
        {
            return _reference.InvokeUnmarshalled<byte[], string, string, string>("createObjectURL", buffer, name, type);
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
