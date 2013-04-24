using System;
using System.Collections.Generic;
using System.Linq;
using FubuCore;
using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Resources.Conneg;
using FubuMVC.Export;

namespace FubuMVC.SlickGrid.Export
{
    public class GenericActions<T> where T : class
    {
        public T get_some_action()
        {
            return null;
        }
    }

    [ConfigurationType(ConfigurationType.Instrumentation)]
    public class ApplySlickGridExcelWriters : IConfigurationAction
    {
        //private static readonly Lazy<IEnumerable<Type>> Mappings;
        private static readonly Lazy<IEnumerable<Type>> GridDefinitions;

        static ApplySlickGridExcelWriters()
        {
            //Mappings = new Lazy<IEnumerable<Type>>(findMappings);
        }

        public void Configure(BehaviorGraph graph)
        {
            var settings = graph.Settings.Get<SlickGridExcelExportSettings>();

            var outputType = typeof (IDictionary<string, object>);

            graph.Behaviors
                .Each(behavior =>
                    behavior.Calls
                        .Where(c => c.HasOutput && c.OutputType() == outputType)
                        .Each(action =>
                        {
                            settings.GridDefinitions.Each(gridType =>
                            {
                                var resourceType = gridType.BaseType.GetGenericArguments()[0];
                                var handlerResourceType = action.HandlerType.GetGenericArguments()[0];
                                if (resourceType == handlerResourceType)
                                {
                                    var mapping = typeof (SlickGridExcelMapping<,>).MakeGenericType(outputType, gridType);
                                    var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping,
                                        outputType);
                                    behavior.Output.Writers.AddToEnd(node);
                                }
                            });
                        }));

            //Mappings.Value.Each(mapping =>
            //{
                ////var type = mapping.BaseType.BaseType.BaseType.GetGenericArguments()[0];
                //var type = mapping.FindParameterTypeTo(typeof (SlickGridExcelMapping<>));

                //Type runnerType = typeof (GenericActions);

                //MethodInfo method = runnerType.GetMethod("get_some_endpoint");

                //var call = new ActionCall(runnerType, method);
                //var chain = new BehaviorChain();
                ////chain.AddToEnd(new SomeNode());
                //chain.AddToEnd(call);
                //chain.Route = new RouteDefinition(DiagnosticConstants.UrlPrefix);
                //chain.Route.Append("_data");
                //chain.Route.Append(type.Name);
                //chain.Route.Append("Export");

                //chain.MakeAsymmetricJson();

                //var node = typeof (ExcelWriterNode<>).CloseAndBuildAs<WriterNode>((object) mapping, type);
                //chain.Output.Writers.InsertFirst(node);

                //graph.AddChain(chain);
            //});
        }

        //private static IEnumerable<Type> findMappings()
        //{
        //    var types = TypePool.AppDomainTypes();

        //    var mappings = types.TypesMatching(type => type.Closes(typeof (SlickGridExcelMapping<>)) && !type.IsAbstract);
        //    return mappings;
        //}
    }
}
