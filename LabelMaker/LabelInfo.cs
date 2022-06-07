using System;
using System.Text;

namespace LabelMaker
{
    public class LabelInfo
    {
        public int    LN  { get; set; }
        public string PN  { get; set; }
        public string SN  { get; set; }
        public string DES { get; set; }
        public string REV { get; set; }
        public string CNT { get; set; }
        public string OLD_PN { get; set; }
        public string BRK { get; set; }
        public string SE { get; set; }
        public string RT { get; set; }
        public LabelInfo(int ln, string pn, string sn, string des, string rev, string cnt, string old_pn, string brk, string se, string rT_s)
        {
            LN  = ln;
            PN  = pn;
            SN  = sn;
            DES = des;
            REV = rev;
            CNT = cnt;
            OLD_PN = old_pn;
            BRK = brk;
            SE = se;
            RT = rT_s;
        }
    }
}