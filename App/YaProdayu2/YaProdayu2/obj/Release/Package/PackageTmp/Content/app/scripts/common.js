$(document).ready(function () {
    showLogin();
});

function showLogin() {
    $('#login').on('click', function () {
        $('#loginForm').modal();
    });
}