﻿var videoUrl;

export function createObjectURL(data, name, type) {
    var buffer = base64ToArrayBuffer(data);
    const file = new File([buffer], name, { type: type });

    buffer = null;

    videoUrl = URL.createObjectURL(file);

    return videoUrl;
};

export function dispose() {
    URL.revokeObjectURL(videoUrl);
    DotNet.disposeJSObjectReference(this);
};

function base64ToArrayBuffer(base64) {
    var binary_string = window.atob(base64);
    var len = binary_string.length;
    var bytes = new Uint8Array(len);

    for (var i = 0; i < len; i++) {
        bytes[i] = binary_string.charCodeAt(i);
    }

    return bytes.buffer;
}