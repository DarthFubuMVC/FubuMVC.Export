using System.Collections.Generic;
using FubuMVC.Export;

namespace ExportHarness.People
{
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

    public class PersonMapping : ExcelMapping<Person, Friend>
    {
        public PersonMapping()
        {
            Column(c => c.Name);
            Source<PersonSource>();
        }
    }

    public class PersonSource : IExcelSource<Person, Friend>
    {
        public IEnumerable<Friend> Rows(Person model)
        {
            return model.Friends;
        }
    }
}