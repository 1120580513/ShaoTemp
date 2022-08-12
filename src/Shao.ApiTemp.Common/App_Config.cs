namespace Shao.ApiTemp.Common
{
    public static partial class App
    {
        public static class Config
        {
            public static string ConnStr => _config.GetSection("ConnectionStrings:Default").Value;
        }
    }
}
