using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shadowsocks.Model
{
    public class SegmentIPOrderList
    {

        List<SegmentIP> _list = new List<SegmentIP>();

        bool IsOrdered = true;

        public bool IsReverse { get; set; }

        public void Add(SegmentIP ipr)
        {
            IsOrdered = false;

            _list.Add(ipr);
        }

        List<SegmentIP> GetDistinct(List<SegmentIP> undistinct)
        {
            var list = new List<SegmentIP>();
            SegmentIP last = undistinct[0];
            list.Add(last);
            for (int i = 1; i < undistinct.Count; i++)
            {
                var ipmask = undistinct[i];
                if (ipmask.Compare(last) == 0)
                    continue;

                list.Add(ipmask);
                last = ipmask;

            }
            return list;

        }
        public void SortList()
        {
            if (IsOrdered)
                return;
            _list = _list.OrderBy((ipr) => ipr, new CmpIPRange()).ToList();
            _list = GetDistinct(_list);
            IsOrdered = true;
        }

        bool IsInList_notrevers(IPAddress ip)
        {
            if (_list.Count == 0)
                return false;

            if (!IsOrdered)
            {
                SortList();
            }


            int start = 0; int end = _list.Count - 1;
            while (true)
            {
                if (start == end)
                {
                    return _list[start].Compare(ip) == 0;
                }
                else if (start + 1 == end)
                {
                    return (_list[start].Compare(ip) == 0) || (_list[end].Compare(ip) == 0);
                }

                var mid = (start + end) / 2;
                {
                    try
                    {
                        var compare1 = _list[mid].Compare(ip);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(1);
                    }

                }
                var compare = _list[mid].Compare(ip);
                if (compare > 0)
                {
                    end = mid;
                    continue;
                }
                else if (compare < 0)
                {
                    start = mid;
                    continue;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsInList(IPAddress ip)
        {
            var rst = IsInList_notrevers(ip);
            return IsReverse ? !rst : rst;
        }

        internal void LoadChinaIP()
        {
            Load_chn_ipmask();
            Load_apnic_latest();
            SortList();
            Console.WriteLine($"ChinaIP cnt:{_list.Count}");
        }

        void Load_chn_ipmask()
        {
            string absFilePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "chn_ipmask.txt");
            if (File.Exists(absFilePath))
            {
                try
                {
                    using (StreamReader stream = File.OpenText(absFilePath))
                    {
                        while (true)
                        {
                            string line = stream.ReadLine();
                            if (line == null)
                                break;
                            if (line.StartsWith("#"))
                                continue;
                            Add(new SegmentIP(line));
                        }
                    }
                }
                catch
                {
                    return;
                }
            }

            return;
        }



        void Load_apnic_latest()
        {
            string absFilePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "delegated-apnic-latest.txt");
            if (File.Exists(absFilePath))
            {
                try
                {
                    using (StreamReader stream = File.OpenText(absFilePath))
                    {
                        while (true)
                        {
                            string line = stream.ReadLine();
                            if (line == null)
                                break;
                            if (!line.StartsWith("apnic|CN|ipv"))
                                continue;

                            var arr = line.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                            var ipversion = arr[2];
                            var strip = arr[3];
                            var strcnt = Int32.Parse(arr[4]);

                            if (ipversion == "ipv4")
                            {
                                Add(new SegmentIP(strip, ParseToMask(strcnt)));
                            }
                            else if (ipversion == "ipv6")
                            {
                                Add(new SegmentIP(strip, strcnt));
                            }


                        }
                    }
                }
                catch
                {
                    return;
                }
            }
            return;
        }



        private static int ParseToMask(int subnetCnt)
        {
            switch (subnetCnt)
            {
                case 256: return 24;
                case 512: return 23;
                case 1024: return 22;
                case 2048: return 21;
                case 4096: return 20;
                case 8192: return 19;
                case 16384: return 18;
                case 32768: return 17;
                case 65536: return 16;
                case 131072: return 15;
                case 262144: return 14;
                case 524288: return 13;
                case 1048576: return 12;
                case 2097152: return 11;
                case 4194304: return 10;
            }
            Console.WriteLine("exception on ParseToMask({ParseToMask})");
            throw new ArgumentOutOfRangeException();
        }
    }

    class CmpIPRange : IComparer<SegmentIP>
    {
        public int Compare(SegmentIP x, SegmentIP y)
        {
            return x.Compare(y);
        }
    }
}
