using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    [TestClass]
    public class ListenerTest
    {
        const string URL = "https://www.sina.com.cn/";
        [TestMethod]
        public void TestListener_Old()
        {
            UseOldListener();
        }
        void UseOldListener()
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.3319.102 Safari/537.36");
            http.QueryString["rnd"] = Shadowsocks.Util.Utils.RandUInt32().ToString();

            http.Proxy = new WebProxy("localhost", 1080);

            //UseProxy = !UseProxy;
            http.DownloadStringCompleted += http_DownloadStringCompleted;
            http.DownloadStringAsync(new Uri(URL));
        }

        private void http_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
