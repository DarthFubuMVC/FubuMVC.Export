using StructureMap;

namespace ExportHarness
{
    public class WebBootstrapper
    {
        public static IContainer BuildContainer()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<CoreRegistry>();
            });

            return ObjectFactory.Container;
        }
    }
}