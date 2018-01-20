using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSP_PoC
{
    class Block
    {
        public Prompt Prompt { get; set; }

        public void Execute()
        {
            Prompt.ReadMessage();

            Environment.Exit(0);
        }
    }
}
