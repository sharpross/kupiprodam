$(document).ready(function () {
    showLogin();

    $('[id=RegionSelect]').change(function (e) {
        var value = e.target.value;

        $.ajax({
            url: '/Buyer/GetListCityes',
            data: {
                name: value
            },
            method: 'GET',
            success: function (resp) {
              $('[name=City]').empty();
              $('[name=City]').append('<option disabled selected>Выберите город</option>');
              for(var i = 0; i < resp.length; i++){
                var value = '<option>' + resp[i] + '</option>';
                $('[name=City]').append(value);
              }
            }
        });
    });
});

function showLogin() {
    $('#login').on('click', function () {
        $('#loginForm').modal();
    });
}

function About() {
    $('[name=About]').jqte();
}

function NewTender() {
    $('[name=Message]').jqte();
}