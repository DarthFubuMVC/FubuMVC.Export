using System.Collections.Generic;
using ExportHarness.People;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Core.Runtime;
using HtmlTags;

namespace ExportHarness.Infrastructure.Conneg
{
    [ConfigurationType(ConfigurationType.Conneg)]
    public class ApplyPersonHtmlExport : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            var behavior = graph.BehaviorFor<PeopleEndpoint>(c => c.get_person());
            behavior.Output.AddWriter(typeof(PersonHtmlWriter));
        }
    }

    public class PersonHtmlWriter : IMediaWriter<Person>
    {
        private readonly IOutputWriter _writer;

        public PersonHtmlWriter(IOutputWriter writer)
        {
            _writer = writer;
        }

        public void Write(string mimeType, Person resource)
        {
            _writer.WriteHtml(BuildPersonHtml(resource).ToString());
        }

        private HtmlTag BuildPersonHtml(Person person)
        {
            return new DivTag().Append("span", tag => tag.Text(person.Name));
        }

        public IEnumerable<string> Mimetypes {
            get
            {
                yield return "text/html";
            }
        }
    }
}