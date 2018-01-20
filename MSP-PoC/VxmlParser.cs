using System;
using System.Collections.Generic;
using Microsoft.Speech.Recognition;
using System.Xml;

namespace MSP_PoC
{
    class VxmlParser
    {
        SpeechRecognitionEngine RecognitionEngine;

        String documentPath = "C:\\Users\\TwoFa\\source\\repos\\MSP-PoC\\MSP-PoC\\VxmlDialogue.xml";
        List<FormTag> FormList = new List<FormTag>(); 

        public VxmlParser(SpeechRecognitionEngine RecognitionEngine)
        {
            this.RecognitionEngine = RecognitionEngine;
        }

        public List<FormTag> ParseDocument()
        {
            XmlTextReader reader = new XmlTextReader(documentPath);
            XmlNodeType type;
            FormTag FormTag = null;
            String condition = null;

            while (reader.Read())
            {
                type = reader.NodeType;
                if(type == XmlNodeType.Element)
                {
                    if (reader.Name == "form")
                    {
                        FormTag = new FormTag(reader.GetAttribute("id"));
                        FormList.Add(FormTag);
                    }
                    if (reader.Name == "field")
                    {
                        FormTag.Field = new Field(reader.GetAttribute("name"), RecognitionEngine);
                    }
                    if (reader.Name == "block")
                    {
                        FormTag.Block = new Block();
                    }
                    if (reader.Name == "prompt")
                    {
                        if (FormTag.Field != null && FormTag.Field.Prompt == null)
                            FormTag.Field.Prompt = new Prompt(reader.ReadString());
                        if (FormTag.Block != null && FormTag.Block.Prompt == null)
                        {
                           String Message = reader.ReadString();
                           FormTag.Block.Prompt = new Prompt(Message);
                        }

                    }
                    if (reader.Name == "grammar")
                    {
                        FormTag.Field.GrammarXmlFile = reader.GetAttribute("src");
                    }
                    if(reader.Name == "nomatch")
                    {
                        if (FormTag.Field != null && FormTag.Field.NoMatch == null)
                        {
                            reader.ReadToDescendant("prompt");
                            FormTag.Field.NoMatch = new Prompt(reader.ReadString());
                        }
                    }
                    if(reader.Name == "filled")
                    {
                        FormTag.Field.Filled = new Filled();
                    }
                    if (reader.Name == "if")
                    {
                        condition = reader.GetAttribute("cond").Split('\'', '\'')[1];
                    }
                    if (reader.Name == "elseif")
                    {
                        condition = reader.GetAttribute("cond").Split('\'', '\'')[1];
                    }
                    if (reader.Name == "goto")
                    {
                        FormTag.Field.Filled.ConditionsDictionary.Add(condition, reader.GetAttribute("next").Trim(new Char[] { '#' }));
                        condition = "default";
                    }
                }

            }
            return FormList;
        }
    }
}
