using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMaker.Accedian
{
	public class AccedianInfo
	{
		public int LN { get; set; }
		public string SN { get; set; }
		public string PN { get; set; }
		public string PartDes { get; set; }
		public string LabelDes { get; set; }
        public string Country { get; set; }
        public string OLDPN { get; set; }

		public AccedianInfo(int ln, string sn, string pn, string pd, string ld, string ct, string old)
		{
			LN = ln;
			SN = sn;
			PN = pn;
			PartDes = pd;
			LabelDes = ld;
            Country = ct;
            OLDPN = old;
		}

		// Overload to allow csv parsing since OLD part number is not saved
		public AccedianInfo(int ln, string sn, string pn, string pd, string ld, string ct)
		{
			LN = ln;
			SN = sn;
			PN = pn;
			PartDes = pd;
			LabelDes = ld;
			Country = ct;
			OLDPN = "";
		}
	}
}
