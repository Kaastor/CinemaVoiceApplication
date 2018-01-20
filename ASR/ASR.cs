using Microsoft.Speech.Recognition;
using Microsoft.Speech.Recognition.SrgsGrammar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ASR_Module
{
    public class ASR
    {
        public static SpeechRecognitionEngine RecognitionEngine;
        public static string GrammarFilePath;
        public static string GrammarFile;
        public static SrgsDocument grammarDoc;

        public static SpeechRecognitionEngine Initialization()
        {
            RecognitionEngine = new SpeechRecognitionEngine(
                new System.Globalization.CultureInfo("pl-PL"));
            RecognitionEngine.SetInputToDefaultAudioDevice();
            return RecognitionEngine;
        }

        public static void BuildGrammar(String GrammarFileName)
        {
            GrammarFile = GrammarFileName;
            GrammarFilePath = "C:\\Users\\TwoFa\\source\\repos\\MSP-PoC\\ASR\\XmlFiles\\" + GrammarFileName;

            Console.WriteLine(GrammarFileName);

            RecognitionEngine.UnloadAllGrammars();
            grammarDoc = new SrgsDocument(GrammarFilePath);
            Grammar grammar = new Grammar(grammarDoc);
            RecognitionEngine.LoadGrammar(grammar);
        }

        public static void StartRecognition()
        {
            Console.WriteLine("Recognition Started");
            RecognitionEngine.RecognizeAsync();
        }

        public static string[] GetGrammarOptions()
        {
            List<string> options = new List<string>();
            XmlTextReader reader = new XmlTextReader(GrammarFilePath);
            XmlNodeType type;
            while (reader.Read())
            {
                type = reader.NodeType;
                if (type == XmlNodeType.Element)
                {
                    if (reader.Name == "item")
                    {
                        options.Add(reader.ReadString());
                    }
                }
            }
            return options.ToArray();
        }

    }
}
