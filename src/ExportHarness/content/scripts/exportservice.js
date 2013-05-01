(function($) {

    function ExportService() {
    }

    ExportService.prototype = {
        exportExcel: function(url) {
            return $.ajax({
                url: url,
                type: 'GET',
                cache: false,
                headers: {
                    Accept: "application/xlsx,application/json; charset=utf-8"
                }
            });
        },
        exportHtml: function(url) {
            return $.ajax({
                url: url,
                type: 'GET',
                headers: {
                    Accept: "text/html; charset=utf-8"
                }
            });
        }
    };

    $.extend(true, $, {
        'ExportService': ExportService
    });
})(jQuery);