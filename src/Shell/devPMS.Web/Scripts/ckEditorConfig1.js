CKEDITOR.editorConfig = function (config) {
    config.toolbarGroups = [
         { name: 'document', groups: ['mode', 'document', 'doctools'] },
         { name: 'clipboard', groups: ['clipboard', 'undo'] },
         { name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
         { name: 'forms', groups: ['forms'] },
         { name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
         { name: 'colors', groups: ['colors'] },
         { name: 'insert', groups: ['insert'] },
         { name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
         { name: 'links', groups: ['links'] },
         { name: 'styles', groups: ['styles'] },
         { name: 'tools', groups: ['tools'] },
         { name: 'others', groups: ['others'] },
         { name: 'about', groups: ['about'] }
    ];
    config.removeButtons = 'Save,Source,NewPage,Preview,Print,Templates,PasteText,PasteFromWord,Find,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,RemoveFormat,JustifyLeft,JustifyCenter,JustifyRight,JustifyBlock,BidiLtr,Language,BidiRtl,Image,Flash,HorizontalRule,Smiley,SpecialChar,PageBreak,Iframe,About,CreateDiv,Cut,Copy,Paste,Replace,SelectAll,Strike,Subscript,Superscript,Outdent,Indent,Anchor,Table,Styles,Format,Font,FontSize';
    config.extraPlugins = 'autogrow,codesnippet';
    config.autoGrow_minHeight = 50;
    config.autoGrow_onStartup = true;
    config.resize_enabled = false;
    config.filebrowserUploadUrl;
    //config.toolbarLocation = 'bottom';
};
CKEDITOR.plugins.registered['save'] = {
    init: function (editor) {
        var command = editor.addCommand('save',
        {
            modes: { wysiwyg: 1, source: 1 },
            exec: function (editor) { // Add here custom function for the save button
                alert('You clicked the save button in CKEditor toolbar!');
            }
        });
        editor.ui.addButton('Save', { label: 'Submit', command: 'save' });
    }
}