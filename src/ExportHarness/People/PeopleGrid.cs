using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuMVC.SlickGrid;

namespace ExportHarness.People
{
    public class PeopleGrid : GridDefinition<Person>
    {
        public PeopleGrid()
        {
            Column(c => c.Name).Title("Name");
            SourceIs<PeopleSource>();
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