function downloadFromUrl (options) {
    var _a;
    var anchorElement = document.createElement('a');
    anchorElement.href = options.url;
    anchorElement.download = (_a = options.fileName) !== null && _a !== void 0 ? _a : '';
    anchorElement.click();
    anchorElement.remove();
}

window.interop = {

    toggleModal: function (modalId) {
        $("#" + modalId).modal("toggle");
    },

    downloadFromByteArray: function (options) {
        var url = "data:" + options.contentType + ";base64," + options.byteArray;
        downloadFromUrl({ url: url, fileName: options.fileName });
    },


    jsonEditor: function (containerId, inputId) {
        var editor = null;
        var container = document.getElementById(containerId);
        var input = document.getElementById(inputId);
        try {
            var value = input.value && input.value != '' ? input.value : null; 
            var data = JSON.parse(value);
            var options = {
                mode: 'code',
                modes: ['code', 'form', 'text', 'tree', 'view', 'preview'],
                onChange:function(json){
                    if(json == null || json === undefined)
                    {
                        var event = new Event('change');
                        input.value = null;
                        input.dispatchEvent(event);
                    }
                },
                onValidate: function (json) {
                    var input = document.getElementById(inputId);
                    var event = new Event('change');
                    input.value = JSON.stringify(json);
                    input.dispatchEvent(event);
                }
            };
            
            editor = new JSONEditor(container, options, data);

        } catch (e) {
            console.log(`${e.name}: ${e.message}`);
        }
    }
}