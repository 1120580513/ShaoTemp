namespace Shao.ApiTemp.Common
{
    public static partial class App
    {
        public static partial class Config
        {
            public static string ConnStr => _config.GetSection("ConnectionStrings:Default").Value;
        }
        public static partial class Config
        {
            public static class Mq
            {
                public static string Uri => _config.GetSection("Mq:Uri").Value;
            }
        }
    }
}
