
$(document).ready(function () {


});


toastr.options = {
    "positionClass": "toast-top-right",
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "preventDuplicates": true,
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut",
};


function validateAndSubmit() {
    var username = $('#Username').val();
    var password = $('#Password').val();

    if (!username || !password) {
        $('#Username, #Password').addClass('is-invalid');
        return;
    }


    $.ajax({
        url: '/AdmiLogin/Login',
        type: 'POST',
        dataType: 'json',
        data: {
            Username: username,
            Password: password
        },
        success: function (result) {
            if (result.success) {
                debugger;
                displaySuccess('Login successful');
                window.location.href = '/DashBoard/DashBoard';
            } else {
                console.log('Login failed');
                displayError('Invalid credentials');
                $('#errorMessage').text('Invalid credentials');
            }
        },
        error: function (error) {
            console.error('AJAX Error:', error);
            $('#errorMessage').text('An error occurred.');
        }
    });
}
function displaySuccess(message) {
    toastr.options.timeOut = 3000;
    toastr.options.extendedTimeOut = 1000;
    toastr.success(message, "Success", { "timeOut": 3000, "class": "toast-success" });
}
function displayError(message) {
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.error(message, "Error", { "timeOut": 0 });
}



function showToaster(message, type = 'success') {
    if (typeof message === 'object') {
        toastr[type](`${message.error}: ${message.message}`);
    } else {
        toastr[type](message);
    }
}
