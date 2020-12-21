window.videoServiceReference = () => {
    return {
        createObjectURL: function (data, name, type) {
            const contentArray = Blazor.platform.toUint8Array(data);
            const nameStr = BINDING.conv_string(name);
            const contentTypeStr = BINDING.conv_string(type);

            const file = new File([contentArray], nameStr, { type: contentTypeStr });

            return BINDING.js_to_mono_obj(URL.createObjectURL(file));
        },
        dispose: function () {
            DotNet.disposeJSObjectReference(this);
        }
    };
}