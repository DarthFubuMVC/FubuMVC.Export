using System.Collections.Generic;
using FubuMVC.SlickGrid;
using FubuMVC.SlickGrid.Export;

namespace ExportHarness.People
{
    [ExcelExport]
    public class PeopleGrid : GridDefinition<Person>
    {
        public PeopleGrid()
        {
            Column(c => c.Name).Title("Name");
            SourceIs<PeopleSource>();

            this.EnableExcelExport();
        }
    }

    public class PeopleSource : IGridDataSource<Person>
    {
        public IEnumerable<Person> GetData()
        {
            return new List<Person>
            {
                new Person
                {
                    Name = "Joe",
                    Friends = new List<Friend>
                    {
                        new Friend
                        {
                            Name = "Jaime"
                        }
                    }
                }
            };
        }
    }
}