//
var _scopeId = 0;

function ServiceSelected(id, scopeName) {
    // title 
    $('#wizard-title').text("Create Project - " + scopeName + " Service");
    //
    _scopeId = id;
    // ScopeId element
    $('#ScopeId').val(_scopeId);
    showWizard();
    iniWizard();
    //$.ajax({
    //    url: '/Project/GetSubTask?id=' + id,
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    error: function () {
    //        alert('Server access failure!');
    //    },
    //    success: function (result) {
    //        if (result != null) {
    //            //GetDataSource(result);
    //            $('#wizard-title').text("Create Project - " + scopeName + " Service");
    //        }
    //        else {
    //            alert("Null, data not found");
    //        }
    //    }
    //});
};

//show wizard
function showWizard() {
    $('.box-primary').addClass('collapsed-box');
    $('.fa').removeClass('fa-minus').addClass('fa-plus');
    $('.box-body').attr('style', 'none');
    $('#wizardform').removeClass('collapsed-box');
    $('#wizardform').removeClass('hidden');
    $('#wizardpanelicon').removeClass('fa-plus').addClass('fa-minus');
}

function iniWizard() {
    // Initialize Smart Wizard  
    if ($("#wizard").length) {
        $('#wizard').smartWizard("reset");
    }

    var template = $("#wizard_template").clone();
    template.attr("id", "wizard");
    template.show();
    // remove old content
    $("#wizardContainer").empty();
    template.appendTo("#wizardContainer");
   
    // maybe we don't need the line below. The wizard re-load whenever select a service.
    //$('#_WizardProjectForm').find("input, textarea").val("");

    var btnFinish = $('<button></button>').text('Finish')
                   .addClass('btn btn-info sw-btn-fin')
                   .prop("disabled", true)
                   .on('click', function () {
                       //alert('Finish button click');
                       //$('#_WizardProjectForm').submit();
                       if (!$(this).hasClass('disabled')) {
                           var elmForm = $("#_WizardProjectForm");
                           if (elmForm) {
                               elmForm.validator('validate');
                               var elmErr = elmForm.find('.has-error');
                               if (elmErr && elmErr.length > 0) {
                                   //alert('Oops we still have error in the form');
                                   return false;
                               } else {
                                   //alert('Great! we are ready to submit form');
                                   elmForm.submit();
                                   return false;
                               }
                           }
                       }
                   });
    var btnCancel = $('<button></button>').text('Reset')
                    .addClass('btn btn-danger sw-btn-can')
                    .on('click', function () {
                        $('#wizard').smartWizard("reset");
                        $('#_WizardProjectForm').find("input, textarea").val("");
                    });

    var wizard = $('#wizard').smartWizard({
        //onLeaveStep: leaveAStepCallback,
        //onFinish: onFinishCallback,
        selected: 0, 
        theme: 'dots',
        transitionEffect: 'fade', // Effect on navigation, none/slide/fade
        //transitionSpeed: '400',
        toolbarSettings: {
            toolbarPosition: 'top', // none, top, bottom, both
            toolbarButtonPosition: 'right', // left, right
            showNextButton: true, // show/hide a Next button
            showPreviousButton: true, // show/hide a Previous button
            toolbarExtraButtons: [btnFinish, btnCancel]
        },
        anchorSettings: {
            markDoneStep: true, // add done css
            markAllPreviousStepsAsDone: true, // When a step selected by url hash, all previous steps are marked done
            removeDoneStepOnNavigateBack: false, // While navigate back done step after active step will be cleared
            enableAnchorOnDoneStep: true // Enable/Disable the done steps navigation
        },
        includeFinishButton: false,
        //onShowStep: onShowStepCallBack,
        contentURL: '/Project/LoadContent?scopeId=' + _scopeId //ajax content load
    });
           
    // Initialize the showStep event
    wizard.on("showStep", function (e, anchorObject, stepNumber, stepDirection) {
        if (stepNumber === 1) {
            $('#ProjectDescription_summernote').summernote({
                height: 300
            });
        }
        if (stepNumber === 4){
            $('#wizard .sw-btn-fin').prop('disabled', false);
            
        }
        else {
            $('#wizard .sw-btn-fin').prop('disabled', true);
        }
    });

    wizard.on("leaveStep", function (e, anchorObject, stepNumber, stepDirection) {
        var x = stepNumber + 1;
        var elmForm = $("#step-" + x);
        // stepDirection === 'forward' :- this condition allows to do the form validation 
        // only on forward navigation, that makes easy navigation on backwards still do the validation when going next
        if (stepDirection === 'forward' && elmForm) {
            return validateSteps(elmForm);
        }
        return true;
    });

    //function onShowStepCallBack(obj, context) {
    //    if (context.toStep == "2") {
    //        $('#ProjectDescription_summernote').summernote({
    //            height: 300
    //        });
    //    }
    //}

    //function leaveAStepCallback(obj, context) {
    //    alert("Leaving step " + context.fromStep + " to go to step " + context.toStep);
    //    return validateSteps(context.fromStep); // return false to stay on step and true to continue navigation 
    //}

    //function onFinishCallback(objs, context) {
    //    if (validateAllSteps()) {
    //        $('form').submit();
    //    }
    //}

    // Your Step validation logic
    function validateSteps(elmFormstep) {
        var isStepValid = true;
        elmFormstep.validator('validate');
        var elmErr = elmFormstep.find('.has-error');
        if (elmErr && elmErr.length > 0) {
            // Form validation failed
            isStepValid = false;
        }
        //// validate step 1
        //if (stepnumber == 1) {
        //    // Your step validation logic
        //    // set isStepValid = false if has errors

        //}
        //// ...      
        return isStepValid;
    }
    //function validateAllSteps() {
    //    var isStepValid = true;
    //    // all step validation logic 
    //    return isStepValid;
    //}
           
};
        
