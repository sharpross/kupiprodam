$(document).ready(function () {
    showLogin();
});

function showLogin() {
    $('#login').on('click', function () {
        $('#loginForm').modal();
    });

    $('.modal').on('shown.bs.modal', function () {
        $(this).find('.modal-dialog').css({
            'margin-top': function () {
                return -($(this).outerHeight() / 2);
            },
            'margin-left': function () {
                return -($(this).outerWidth() / 2);
            }
        });
    });
}