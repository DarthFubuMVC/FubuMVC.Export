#FubuMVC.Export#
Simple Excel exports.

* [Setup an Endpoint](#setup-an-endpoint)
* [Create the Mapping](#create-the-mapping)
* [Create the Excel Source](#create-the-excel-source)
* [Create a JavaScript Service](#create-a-javascript-service)
* [A Sample JavaScript Controller That Uses the Service](#a-sample-javascript-controller-that-uses-the-service)
* [How it Works](#how-it-works)
* [Dependencies](#dependencies)
* [Gotchas](#gotchas)

#Setup an Endpoint#
Create an `EndPoint` with an `Action` that returns the resource you want to export.  Our example will be a `Person` with `Friends`.

    public class Person
    {
        public string Name { get; set; }
        public List<Friend> Friends { get; set; }
    }

    public class Friend
    {
        public string Name { get; set; }
    }

    public class PeopleEndpoint
    {
        public Person get_person()
        {
            return new Person
            {
                Name = "Joe",
                Friends = new List<Friend>
                {
                    new Friend
                    {
                        Name = "Jaime"
                    }
                }
            };
        }
    }

#Create the Mapping#
Create an `ExcelMapping<T>` or `ExcelMapping<T,K>` for what you want exported.  For example, if you use `ExcelMapping<Person>`, you will be exporting a list of `Person` objects.  If you use `ExcelMapping<Person, Friend>` you will be exporting a list of `Friend` objects.

You can define as columns which properties you want exported.

    public class PersonMapping : ExcelMapping<Person, Friend>
    {
        public PersonMapping()
        {
            Column(c => c.Name);
            Source<PersonSource>();
        }
    }


#Create the Excel Source#
Create an `IExcelSource<T>` or `IExcelSource<T,K>` to provide the source for the export.  Remember to set that source in the `ExcelMapping`.  The resource returned from the Endpoint will be passed to the `Rows` method, which you can use to determine what to return.  You can imagine that the `IExcelSource` could also hit a data store to retrieve additional data based on the given resource.

    public class PersonSource : IExcelSource<Person, Friend>
    {
        public IEnumerable<Friend> Rows(Person model)
        {
            return model.Friends;
        }
    }
    
#Create a JavaScript Service#
Create a service that can make a call to the defined `EndPoint` `Action`.  FubuMVC.Export uses Content Negotiation to determine when to create the Excel file.  Notice the `Accept` header it set to both `application/xlsx` and `application/json`.  Both of these `Accept` values are required.

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
        }
    };

#A Sample JavaScript Controller That Uses the Service#
Here is a sample JavaScript Controller that uses the defined service.  The `exportPeople` function calls the service sending the url of the `Action`, `/person`.  If the result was a success, it sends the given `fileUrl` to the `download` function.  The `download` function creates an `<iframe>` to download the file.  The `NameToDisplay` parameter is optional.

    function ExportController(service) {
        this.service = service;
    }

    ExportController.prototype = {
        exportPeople: function () {
            var that = this;
            this.service
                .exportExcel('/person')
                .done(function (result) {
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
    
#How it Works#
FubuMVC.Export will scan your route outputs looking for a matching `ExcelMapping<T>` or `ExcelMapping<T,K>`.  If it finds one, it will wire up a `ExcelMediaWriter<T>` as an available output.  In our example, our route returns a `Person` object, so it will look for a matching `ExcelMapping<Person>` or `ExcelMapping<Person,K>`.  The matching process ignores the `K` generic parameter.  Since it found a mapper, it will create a `ExcelMediaWriter<Person>`.  When you make a http request with the `Accept Header` set to `application/xlsx` it will hit that wired up `ExcelMediaWriter`.

When the `Action`, in our case `get_person`, is requested the `ExcelMediaWriter` will write a temporary file to disk, and return a url that can be used to download the file.  FubuMVC.Export automatically wires up a download `EndPoint` with an `Action` which returns a `DownloadFileModel`.  By default this looks like `file/download/{filenamehash}`.  When the `file/download` `Action` is requested with a valid filename hash it will download the file.

#Dependencies#
FubuMVC.Export uses the `DocumentFormat.OpenXml` nuget to do the Excel generation.  `DocumentFormat.OpenXml` depends on `WindowsBase`.

#Gotchas#
Internet Explorer will cache ajax requests when using jQuery.  When doing an ajax requests with jQuery to download the Excel file, make sure to set `cache:false` on the request.
