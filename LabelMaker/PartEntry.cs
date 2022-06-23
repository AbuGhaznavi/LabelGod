using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelMaker
{
    class PartEntry
    {
        public string Line { get; set; }
        public string Part { get; set; }

        public string Serial { get; set; }
        public string Description { get; set; }
        public string Revision { get; set; }
        public string Country { get; set; }
        public string Old_Part { get; set; }
        public string Breakout_Quantity { get; set; }
        public string Second_End{ get; set; }
       

        public PartEntry(string _Line, string _Part, string _Serial, string _Description, string _Revision, string _Country, string _Old_Part, string _Breakout_Quantity, string _Second_End)
        {
            Line = _Line;
            Part = _Part;
            Serial = _Serial;
            Description = _Description;
            Revision = _Revision;
            Country = _Country;
            Old_Part = _Old_Part;
            Breakout_Quantity = _Breakout_Quantity;
            Second_End = _Second_End;
        }

    }
}
