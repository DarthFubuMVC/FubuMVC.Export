using System;
using System.Collections.Generic;
using System.Reflection;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Export;
using FubuMVC.SlickGrid;
using FubuMVC.SlickGrid.Export;

namespace ExportHarness.People
{
    public static class GridExtensions
    {
        public static void EnableExcelExport<T>(this GridDefinition<T> definition) where T : class 
        {
            definition.ChainBehavior((graph, chain, sourceType, runnerType) =>
            {
                var type = typeof (IDictionary<string, object>);

                var mapping = typeof (SlickGridExcelMapping<>).MakeGenericType(type);
                //var type = typeof (T);

                //MethodInfo method = runnerType.GetMethod("Run");

                //var call = new ActionCall(runnerType, method);
                //var newChain = new BehaviorChain();
                //newChain.AddToEnd(call);
                ////newChain.Route = new RouteDefinition(chain.Route.Pattern);
                ////newChain.Route.Append("Export");

                //newChain.Route = new RouteDefinition(DiagnosticConstants.UrlPrefix);
                //newChain.Route.Append("_data");
                //newChain.Route.Append("Export");
                //newChain.Route.Append(typeof (T).Name);

                var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping, type);
                chain.Output.Writers.InsertFirst(node);

                //graph.AddChain(newChain);
            });
        }
    }

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