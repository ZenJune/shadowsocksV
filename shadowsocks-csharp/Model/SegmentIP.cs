using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shadowsocks.Model
{
    public class SegmentIP
    {
        private byte[] _ipBs;
        private IPAddress _ip;
        private int _mask;
        public SegmentIP(string ipwithmask)
        {
            var arr = ipwithmask.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr.Count() == 1)
            {
                _ip = IPAddress.Parse(arr[0]);
                _mask = _ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? 32 : 128;
            }
            else if (arr.Count() == 2)
            {
                _ip = IPAddress.Parse(arr[0]);
                _mask = Int32.Parse(arr[1]);
            }
            else
            {
                var err = $"Wrong Formate! {ipwithmask}";
                Console.WriteLine(err);
                throw new FormatException(err);
            }

            _ipBs = _ip.GetAddressBytes();
        }
        public SegmentIP(string strip, int mask)
        {
            _ip = IPAddress.Parse(strip);
            _mask = mask;
            _ipBs = _ip.GetAddressBytes();
        }
        public SegmentIP(IPAddress ip, int mask)
        {
            _ip = ip; _mask = mask;
            _ipBs = _ip.GetAddressBytes();
        }

        /// <summary>
        /// bigger :1   
        /// equ:0 
        /// smaller:-1
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public int Compare(IPAddress ip)
        {
            var isMatch = IsByteArrMatch(_ipBs, ip.GetAddressBytes(), _mask);
            return isMatch;
        }
        /// <summary>
        /// A>B :1   
        /// A=B:0 
        /// A<B:-1
        /// </summary>
        /// <param name="bsA"></param>
        /// <param name="bsB"></param>
        /// <param name="cntBit"></param>
        /// <returns></returns>
        int IsByteArrMatch(byte[] A, byte[] B, int cntBit)
        {
            var byteCmpCnt = cntBit / 8;
            for (int i = 0; i < byteCmpCnt; i++)
            {
                if (A[i] > B[i])
                    return 1;
                else if (A[i] == B[i])
                    continue;
                else
                    return -1;
            }

            cntBit -= byteCmpCnt * 8;
            if (cntBit == 0)
            {
                return 0;
            }

            var otherA = A[byteCmpCnt] >> (8 - cntBit);
            var otherB = B[byteCmpCnt] >> (8 - cntBit);
            if (otherA > otherB)
                return 1;
            else if (otherA == otherB)
                return 0;
            else
                return -1;

        }

        public int Compare(SegmentIP iprange)
        {
            var bitcmp = Math.Max(_mask, iprange._mask);
            var rst = IsByteArrMatch(_ipBs, iprange._ipBs, bitcmp);
            return rst;
        }



        static public bool operator >(SegmentIP iprA, SegmentIP iprB)
        {
            return iprA.Compare(iprB) == 1;
        }
        static public bool operator <(SegmentIP iprA, SegmentIP iprB)
        {
            return iprA.Compare(iprB) == -1;
        }
   
        public override string ToString()
        {
            return $"{_ip}/{_mask}";
        }

       
    }
}
