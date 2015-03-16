$(document).ready(function () {
    var max = 10;

    $('input.subCheck').on('click', function (el) {
        var selected = $('input.subCheck');

        var sel = 0;

        for (var i = 0; i < selected.length; i++) {
            if (el.target != selected[i]) {
                if (selected[i].checked == true) {
                    sel++;
                }

                if (sel == max) {
                    break;
                }
            }
        }

        if (sel == max) {
            el.target.checked = false;
        }
    });
});

function UpdateSubsciptions() {
    var subs = [];

    var selected = $('input.subCheck');

    for (var i = 0; i < selected.length; i++) {
        if (selected[i].checked == true) {
            subs.push(selected[i].value.toString());
        }
    }

    $.ajax({
        url: '/Account/UpdateSubsciptions',
        data: JSON.stringify({ subs: subs }),
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (resp) {

        }
    });
}