using System;
using System.Collections.Generic;
using System.Text;

namespace XML_Parser
{
    class Model
    {
        public int id, bid;
        public int? cbid;
        public string Cbid { 
            set 
            { 
                if (value == null)
                {
                    cbid = null;
                }
                else
                {
                    cbid = int.Parse(value);
                }
            } 
        }
        public bool available;
        public string type;
    }
}
