using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMaker
{
    class Part
    {
        public string PN { get; set; }
        public string TYP { get; set; }

        public string REV { get; set; }
        public string DAT { get; set; }
        public string WDM { get; set; }
        public string WL { get; set; }
        public string SMMM { get; set; }
        public string DIS { get; set; }
        public string TMP { get; set; }
        public string RT { get; set; }

        public string COMP { get; set; }

        public Part(string pn, string typ, string rev, string dat, string wdm, string wl, string smmm, string dis, string tmp, string rT_s, string comp)
        {
            PN = pn;
            TYP = typ;
            REV = rev;
            DAT = dat;
            WDM = wdm;
            WL = wl;
            SMMM = smmm;
            DIS = dis;
            TMP = tmp;
            RT = rT_s;
            COMP = comp;
        }

    }
}
