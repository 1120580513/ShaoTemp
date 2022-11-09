using Shao.ApiTemp.Common.Mq;

namespace Shao.ApiTemp.Common
{
    public static partial class App
    {
        public static class Mq
        {
            private static IMqClient _current = Resolve<IMqClient>();
            private static IMqClient _erp = Resolve<IMqClient>();

            public static IMqClient Current => _current;
            public static IMqClient ERP => _erp;

            public static void Init()
            {
                Current.Init();
                //ERP.Init();
            }
        }
    }
}
