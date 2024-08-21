class Helper {
    alert(responseCode, message) {
        switch (responseCode) {
            case 100:
                $.toast({
                    heading: 'Information',
                    text: message,
                    icon: 'info',
                    position: 'top-right',
                })
                break;
            case 200:
                $.toast({
                    heading: 'Success',
                    text: message,
                    icon: 'success',
                    position: 'top-right',
                })
                break;
            case 202:
                $.toast({
                    heading: 'Warning',
                    text: message,
                    icon: 'warning',
                    position: 'top-right',
                })
                break;
            case 500:
                $.toast({
                    heading: 'Error',
                    text: message,
                    icon: 'error',
                    position: 'top-right',
                })
                break;
            default:
        }
    }

    validate() {
        let isValid = false;
        $('input:required, select:required').removeClass("is-invalid");
        $('input:required, select:required').addClass("is-valid");
        let inputs = $('input:required,select:required');

        let filteredInputs = inputs.filter(function () {

            let value = $(this).val();
            if (value == "0" || value == "-1") {
                value = '';
            }
            return value === "";
        });

        if (filteredInputs.length > 0) {
            filteredInputs.each(function () {
                isValid = false;
                var input = $(this);
                if (input[0].required) {

                    if (input[0].value != "" && input[0].value != '0' && input[0].value != "0") {
                        input.removeClass("is-invalid");
                        input.addClass("is-valid");

                    }
                    else {
                        input.addClass("is-invalid");
                        input.removeClass("is-valid");

                    }
                }
            });
        }
        else {
            isValid = true;
        }
        return isValid;
    }

    validateTableInputs() {
        var isValid = true;
        $(`#${tableid} tbody tr`).each(function () {
            var inputs = $(this).find('input, select');
            inputs.each(function () {
                if ($(this).is('input') && $(this).val() == '') {
                    isValid = false;
                    return false;
                } else if ($(this).is('select') && $(this).val() == '') {
                    isValid = false;
                    return false;
                }
            });
            if (!isValid) {
                return false;
            }
        });
        return isValid;
    }

    validateForm(obj) {
        let isValid = true;
        $.each(obj, function (key, value) {
            let trimmedValue = $.trim(value);
            if (trimmedValue === '') {
                alertError(key + ' cannot be empty.');
                isValid = false;
                return false;
            }
            if (key === 'EmailId') {
                let emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (!emailRegex.test(trimmedValue)) {
                    alertError('Please enter a valid email address for ' + key);
                    isValid = false;
                    return false;
                }
            }
        });

        return isValid;
    }

    dialog(title, body, size) {
        let hight = 500;
        let width = 500;
        if (size == 'small') {
            hight = 0;
            width = 0;
        }
        if (size == 'medium') {
            hight = 700;
            width = 700;
        }
        if (size == 'large') {
            hight = 1500;
            width = 1500;
        }
        $(`<div id="dialog" title="${title}">${body}</div>`).appendTo("body");
        $("#dialog").dialog({
            width: width,
            hight: hight
        });
    }

    modal(title, body, size) {
        let hight = 500;
        let width = 500;
        if (size == 'small') {
            hight = 0;
            width = 0;
        }
        if (size == 'medium') {
            hight = 700;
            width = 700;
        }
        if (size == 'large') {
            hight = 1500;
            width = 1500;
        }
        $(`<div id="dialog" title="${title}">${body}</div>`).appendTo("body");
        $("#dialog").dialog({
            width: width,
            hight: hight,
            modal: true,
            draggable: false,
        });
    }

    confirm(message, callback) {
        var dialog = $(`<div id="dialog" title="Confirmation" class="row"><div class="col-md-12">
        <h5>${message}</h5>
        </div></div>`).appendTo("body");

        dialog.dialog({
            modal: true,
            draggable: false,
            position: {
                my: "center center",
                at: "center center",
                of: window
            },
            buttons: {
                "Ok": function () {
                    callback();
                    dialog.dialog("close");
                },
                "Cancel": function () {
                    dialog.dialog("close");
                }
            }
        });
    }
}
const helper = new Helper();