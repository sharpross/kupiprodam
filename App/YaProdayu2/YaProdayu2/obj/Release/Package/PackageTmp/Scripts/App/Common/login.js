$(document).ready(function () {
    var loginBtn = $('button[name=LoginButton]');
    var logoutBtn = $('a[name=LogoutButton]');

    if(loginBtn){
        loginBtn.bind('click', function () {
            var login = $('input[name=Login]').val();
            var password = $('input[name=Password]').val();
            var rememberMe = $('input[name=RememberMe]').val();
            $.ajax({
                type: 'POST',
                url: '/Account/Login',
                data: $('form[name=LoginForm]').serialize(),
                success: function (data) {
                    if (data.Success) {
                        window.location.reload();
                    } else {
                        $('.LoginError').text(data.Data)
                    }
                }
            });
        });
    }
    
    if(logoutBtn){
        logoutBtn.bind('click', function () {
            $.ajax({
                type: 'POST',
                url: '/Account/Logout',
                success: function (data) {
                    if (data.Success) {
                        window.location.reload();
                    }
                }
            });
        });
    }
});