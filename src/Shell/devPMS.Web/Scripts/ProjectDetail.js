﻿$(document).ready(function () {
    var branchList = [];
    var ScopeTypeList = [];
    //var servicesDescriptionList = [];
    var PriorityList = [];
    //$.ajax({
    //    url: '/Project/GetBranches',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    error: function () {
    //        alert('Server access failure!');
    //    },
    //    success: function (respond) {
    //        if (respond != null) {
    //            branchList = respond.data;
    //        }
    //        else {
    //            alert("Null, branch list could not load");
    //        }
    //    }
    //})
    var ScopeID = $('#ScopeID').val();

    $.ajax({
        url: '~/Project/ReferenceDataJson?scopeId=' + ScopeID,
        type: 'GET',
        async: false,
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        error: function () {
            alert('Server access failure!');
        },
        success: function (response) {
            if (response !== null) {
                var data = response.data;
                $.each(response.data.BranchList, function (index, obj) {
                    branchList.push({
                        text: obj.Text,
                        value: obj.Value
                    });
                });
                $.each(response.data.ScopeTypeList, function (index, obj) {
                    ScopeTypeList.push({
                        text: obj.Text,
                        value: obj.Value
                    });
                });
                $.each(response.data.PriorityList, function (index, obj) {
                    PriorityList.push({
                        text: obj.Text,
                        value: obj.Value
                    });
                });
                //branchList = response.BranchList;
                //ScopeTypeList = response.ScopeTypeList;
                //PriorityList = response.PriorityList;
            }
            else {
                alert("Null, branch list could not load");
            }
        }
    })
  
    //$.ajax({
    //    url: '/Project/ServiceList',
    //    type: 'GET',
    //    async: false,
    //    dataType: 'json',
    //    contentType: 'application/json; charset=utf-8',
    //    error: function () {
    //        alert('Server access failure!');
    //    },
    //    success: function (respond) {
    //        if (respond != null) {
    //            servicesDescriptionList = respond.data;

    //        }
    //        else {
    //            alert("Null, Subtakses list could not load");
    //        }
    //    }
    //});

    //turn to inline mode
    $.fn.editable.defaults.mode = 'inline';

    $('#Address').editable(
    {
        url: function (params) {
            UpdateDetail(params);
        }
    }
    )
    //function Update2(params) {
    //    url: '/Project/DetailUpdate',
    //    ajaxOptions: {

    //    }
    //};
    $('#EOR').editable(
        {
            url: function (params) {
                return UpdateDetail(params);
            },
            success: function (success, data, config) {
                if (data && data.id) {  //record created, response like {"id": 2}
                    //set pk
                    $(this).editable('option', 'pk', data.id);
                    //remove unsaved class
                    $(this).removeClass('editable-unsaved');
                    //show messages
                    var msg = 'New user created! Now editables submit individually.';
                    $('#msg').addClass('alert-success').removeClass('alert-error').html(msg).show();
                    $('#save-btn').hide();
                    $(this).off('save.newuser');
                } else if (data && data.errors) {
                    //server-side validation error, response like {"errors": {"username": "username already exist"} }
                    config.error.call(this, data.errors);
                }
            },
            error: function (errors) {
                var msg = '';
                if (errors && errors.responseText) { //ajax error, errors = xhr object
                    msg = errors.responseText;
                } else { //validation error (client-side or server-side)
                    $.each(errors, function (k, v) { msg += k + ": " + v + "<br>"; });
                }
                $('#msg').removeClass('alert-success').addClass('alert-error').html(msg).show();
            }
        }

    )
    $('#ProjectName').editable(
        {
            url: function (params) {
                UpdateDetail(params);
            }
        }
        )
    $('#City').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })
    $('#State').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })
    $('#Zip').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })
    $('#ProjectEngineer').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })

    $('#SimpsonDueDate').editable({
        url: function (params) {
            UpdateDetail(params);
        },
        //format: 'YYYY-MM-DD',    
        //viewformat: 'DD/MMM/YYYY',
        combodate: {
                minYear: 2000,
                maxYear: 2030,
                minuteStep: 1
        }
    })
    $('#VNDueDate').editable({
        url: function (params) {
            UpdateDetail(params);
        },
        combodate: {
            minYear: 2000,
            maxYear: 2030,
            minuteStep: 1
        }
    })
    $('#summerNoteInside').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })
    $('#Priority').editable({
        url: function (params) {
            UpdateDetail(params);
        },
        source: PriorityList
    })
    $('#BranchList').editable({
        url: function (params) {
            UpdateDetail(params);
        },
        source: branchList
    })
    $('#ScopeTypeId').editable({
        url: function (params) {
            UpdateDetail(params);
        },
        source:ScopeTypeList
    })
    $('#Contact').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })
    $('#Requestor').editable({
        url: function (params) {
            UpdateDetail(params);
        },
    })
    $('#Reviewer').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })
    //$('#Assignee').editable({
    //    url: function (params) {
    //        UpdateDetail(params);
    //    }
    //})

    $('#Description_editable').editable({
        url: function (params) {
            UpdateDetail(params);
        }
    })

    

    function UpdateDetail(params) {
        var oldValueObj = $(this).editable('getValue');
        $.ajax({
            cache: false,
            async: true,
            type: 'POST',
            data: params,
            url: '/Project/DetailUpdate',

            success: function (data) {
                if (data.success) {
                    new PNotify({
                        title: 'Success',
                        text: 'Project detail updated ',
                        type: 'success',
                        styling: 'bootstrap3'
                    });
                }
                else {
                    new PNotify({
                        title: 'Ops,',
                        text: 'Cannot update',
                        type: 'alert',
                        styling: 'bootstrap3'
                    });
                    //Cannot display an invalid value.
                    //config.error.call(this, "error here ddo!");
                }
            },
            error: function (data) {

                new PNotify({
                    title: 'Ops,',
                    text: 'Action unable to finish ',
                    type: 'error',
                    styling: 'bootstrap3'
                });
                //config.error.call(this, data.errors);
            }

        });
    };
   
})