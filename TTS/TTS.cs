using Microsoft.Speech.Synthesis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTS_Module
{
    public class TTS
    {
        private static SpeechSynthesizer synthesizer;
        public static void Initialization()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SetOutputToDefaultAudioDevice();
        }

        public static void Read(String message)
        {
            synthesizer.SpeakAsync(message);
        }
    }
}
