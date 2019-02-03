var hoursList = (function () {
    let add = function () {
        jQuery.get('/Admin/AddHours').done(function (html) {
            $('#hours-list').append(html);
        });
    };

    return {
        add: add
    };
})();