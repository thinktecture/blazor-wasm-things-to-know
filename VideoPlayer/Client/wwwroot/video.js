window.videoServiceReference = () => {
    return {
        createObjectURL: function (data, name, type) {
            const content = Blazor.platform.toUint8Array(data);
            const fileName = BINDING.conv_string(name);
            const contentType = BINDING.conv_string(type);

            const file = new File([content], fileName, { type: contentType });

            return BINDING.js_to_mono_obj(URL.createObjectURL(file));
        },
        dispose: function () {
            DotNet.disposeJSObjectReference(this);
        }
    };
}