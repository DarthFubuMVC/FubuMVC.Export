(function ($) {

    function SlickGridExportController(service) {
        this.service = service;
    }

    SlickGridExportController.prototype = {
        exportPeople: function () {
            var that = this;
            this.service
                .exportExcel('/_diagnostics/_data/Person')
                .done(function(result) {
                    if (true === result.success) {
                        var src = result.fileUrl + '?NameToDisplay=myexport.xlsx';
                        that.download(src);
                    }
                });
        },
        init: function() {
            var that = this;
            $('.btn-export').on('click', function() {
                that.exportPeople();
            });
        },
        download: function(src) {
            $('<iframe />')
                .attr('src', src)
                .css('visibility', 'hidden')
                .appendTo('body');
        }
    };

    var application = null;
    function bootstrap() {
        if (application) return application;

        var service = new $.ExportService();
        application = new SlickGridExportController(service);
        application.init();
        return application;
    }

    $.extend(true, $, {
       'fubuSlickGridExport': {
           'bootstrap': bootstrap
       } 
    });
})(jQuery);