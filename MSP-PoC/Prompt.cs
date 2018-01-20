using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSP_PoC
{
    class Prompt
    {
        public String Message { get; set; }

        public Prompt() {}

        public Prompt(String message)
        {
            Message = message;
        }

        public void ReadMessage()
        {
            TTS_Module.TTS.Read(Message);
        }
    }
}
