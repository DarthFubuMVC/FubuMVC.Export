﻿(function ($) {

    function ExportController(service) {
        this.service = service;
    }

    ExportController.prototype = {
        exportPeople: function () {
            var that = this;
            this.service
                .exportExcel('/person')
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
            $('.btn-html').on('click', function() {
                that.exportHtml();
            });
        },
        download: function(src) {
            $('<iframe />')
                .attr('src', src)
                .css('visibility', 'hidden')
                .appendTo('body');
        },
        exportHtml: function() {
            this.service
                .exportHtml('/person')
                .done(function (html_results) {
                    console.log('html', html_results);
                    $('.html-export').append(html_results);
                });
        }
    };

    var application = null;
    function bootstrap() {
        if (application) return application;

        var service = new $.ExportService();
        application = new ExportController(service);
        application.init();
        return application;
    }

    $.extend(true, $, {
       'fubuExport': {
           'bootstrap': bootstrap
       } 
    });
})(jQuery);