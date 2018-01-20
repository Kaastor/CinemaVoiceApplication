using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP_PoC
{
    class FormTag
    {
        public Field Field { get; set; }
        public Block Block { get; set; }
        public String Id { get; set; }

        public FormTag(String Id)
        {
            this.Id = Id;
        }

        
    }
}
