﻿using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BlazorWasmVideo.Client
{
    public class VideoServiceUnmarshaledJSInterop : IAsyncDisposable
    {
        private IJSRuntime _jsRuntime;
        private IJSUnmarshalledObjectReference _reference;

        public VideoServiceUnmarshaledJSInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitAsync()
        {
            await _jsRuntime.InvokeVoidAsync("import", "./videoServiceUnmarshaledJSInterop.js");

            var unmarshalledRuntime = (IJSUnmarshalledRuntime)_jsRuntime;
            _reference = unmarshalledRuntime.InvokeUnmarshalled<IJSUnmarshalledObjectReference>("videoServiceUnmarshaledReference");
        }

        public string CreateURLFromBuffer(byte[] buffer, string name, string type)
        {
            return _reference.InvokeUnmarshalled<byte[], string, string, string>("createObjectURL", buffer, name, type);
        }

        public async ValueTask DisposeAsync()
        {
            if (_reference != null)
            {
                _reference.InvokeVoid("dispose");
                await _reference.DisposeAsync();
            }
        }
    }
}
