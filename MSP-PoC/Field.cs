using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Speech.Recognition;
using System.Text;

namespace MSP_PoC
{
    class Field
    {
        private SpeechRecognitionEngine RecognitionEngine;

        public String Name { get; set; }
        public String GrammarXmlFile { get; set; }
        public Prompt NoMatch { get; set; }
        public Prompt Prompt { get; set; }
        public Filled Filled { get; set; }

        public Field(String Name, SpeechRecognitionEngine RecognitionEngine)
        {
            this.Name = Name;
            this.RecognitionEngine = RecognitionEngine;
        }

        public void ExecuteField()
        {
            Prompt.ReadMessage();
            ASR_Module.ASR.BuildGrammar(GrammarXmlFile);
            ASR_Module.ASR.StartRecognition();
        }

    }
}
