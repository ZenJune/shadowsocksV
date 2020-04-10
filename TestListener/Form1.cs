using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestListener
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

        }


        //const string URL = "https://www.sina.com.cn/";
        //   const string URL = "http://bwg.inskyline.top/";
        const string URL = "http://192.168.1.2/";
        Random random = new Random();

        int UseOldListener(bool IsShowHtml = false)
        {
            WebClient http = new WebClient();
            http.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.3319.102 Safari/537.36");
            ServicePointManager.ServerCertificateValidationCallback +=
                (object sender, X509Certificate cert, X509Chain chain, System.Net.Security.SslPolicyErrors error) => true;


           http.Proxy = new WebProxy("localhost", 1080);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var str = http.DownloadString(new Uri(URL+$"?id={random.Next(1000)}"));//反缓存
            sw.Stop();
            if (IsShowHtml)
                Console.WriteLine(str);
            return (int)sw.ElapsedMilliseconds;
        }
        int UseOldListenerNooutput()
        {
            return UseOldListener(IsShowHtml: false);
        }
        int UseOldListenerHasoutput()
        {
            return UseOldListener(IsShowHtml: true);
        }


        private void btnTryOnce_Click(object sender, EventArgs e)
        {
            var taketime = UseOldListener(IsShowHtml: true);
            lblRst.Text = $"cnt:{1} taketime:{taketime}ms";

        }

        private void btnTryMultTime_Click(object sender, EventArgs e)
        {
            var cnt = 100;
            lblRst.Text = "busy...";

            var sumtaketime = TryByCnt(cnt,out int failcnt);
            lblRst.Text = $"cnt:{cnt} taketime:{sumtaketime}ms fail:{failcnt} average:{sumtaketime / cnt}ms";


        }


        int TryByCnt(int trycnt,out int failcnt)
        {
            int sum = 0;
            int completecnt = 0;
            List<Thread> tlist = new List<Thread>();
            for (int i = 0; i < trycnt; i++)
            {
                var t = new Thread(() =>
                {
                    var taketime = UseOldListenerNooutput();
                    Interlocked.Add(ref sum, taketime);
                    Interlocked.Increment(ref completecnt);
                });
                t.Start();
                tlist.Add(t);
            }
           

          
          
            tlist.ForEach(t => t.Join(1000));
            failcnt = trycnt - completecnt;
            return sum;

        }
    }
}
