namespace ExportHarness
{
    public class HomeModel
    {
    }

    public class HomeEndpoint
    {
        public HomeModel Index()
        {
            return new HomeModel();
        }
    }
}