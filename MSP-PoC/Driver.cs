using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Configuration;

namespace MSP_PoC
{
    public partial class Driver : Form
    {
        private const string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\TwoFa\\source\\repos\\MSP-PoC\\MSP-PoC\\Database.mdf;Integrated Security=True";

        private VxmlParser parser;
        private List<FormTag> FormList = null;
        private FormTag CurrentForm = null;
        private FormTag NextForm = null;

        private string UserResponse = "";

        public Driver()
        {
            InitializeComponent();
            ASR_Module.ASR.Initialization();
            TTS_Module.TTS.Initialization();
            ASR_Module.ASR.RecognitionEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SpeechRecognizedEventHandler);
            ASR_Module.ASR.RecognitionEngine.RecognizeCompleted += new EventHandler<RecognizeCompletedEventArgs>(RecognizeCompletedEventHandler);

            parser = new VxmlParser(ASR_Module.ASR.RecognitionEngine);

            FormList = parser.ParseDocument();

            //PrintForms();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Kupno biletów do kina";
            this.BackColor = System.Drawing.Color.Gray;
            QueryDatabaseToTable();
        }

        private void FormInterpretationAlgorithm(FormTag Form)
        {
            CurrentForm = Form;
            if (Form.Field != null)
            {
                Form.Field.ExecuteField();
                DisplayOptionsOnList();
            }
            else
            {
                Form.Block.Execute();
            }
        }

        private void DisplayOptionsOnList()
        {
            choicesList.Items.Clear();
            string[] options = ASR_Module.ASR.GetGrammarOptions();
            foreach (string option in options)
            {
                ListViewItem item = new ListViewItem(option);
                item.SubItems.Add(option);
                choicesList.Items.Add(item);
            }
        }

        private void SpeechRecognizedEventHandler(object sender, SpeechRecognizedEventArgs e)
        {
            ASR_Module.ASR.RecognitionEngine.RecognizeAsyncCancel();
            string speechRecognized = e.Result.Text;
            Console.WriteLine("Speech recognized: " + speechRecognized);

            SaveResponseToDatabase(speechRecognized);
          
            String nextFormId = CurrentForm.Field.Filled.Execute(speechRecognized);
            NextForm = FormList.Find(form => form.Id == nextFormId);
        }

        private void RecognizeCompletedEventHandler(object sender, RecognizeCompletedEventArgs e)
        {
            ASR_Module.ASR.RecognitionEngine.RecognizeAsyncCancel();
            if (e.Result == null)
            {
                Console.WriteLine("Recognize Completed: Nie rozpoznano");
                CurrentForm.Field.NoMatch.ReadMessage();
            }
            FormInterpretationAlgorithm(NextForm);
        }

        private void SaveResponseToDatabase(string speechRecognized)
        {
            if (ASR_Module.ASR.GrammarFile.Equals("Names.xml") ||
               ASR_Module.ASR.GrammarFile.Equals("Movies.xml") ||
               ASR_Module.ASR.GrammarFile.Equals("Dates.xml") ||
               ASR_Module.ASR.GrammarFile.Equals("Numbers.xml"))
                UserResponse += speechRecognized + " ";

            if(speechRecognized.Equals("finalizuję"))
            {
                string query = "insert into Response (UserResponse) values(@UserResponse)";
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@UserResponse", SqlDbType.NVarChar).Value = UserResponse;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void PrintForms()
        {
            foreach (FormTag tag in FormList)
            {
                if (tag.Field != null)
                {
                    Console.Write(tag.Id + ": ");
                    Console.WriteLine(tag.Field.Prompt.Message + "'");
                    Console.WriteLine(tag.Field.GrammarXmlFile + "'");
                    Console.WriteLine(tag.Field.NoMatch.Message + "'");

                    foreach (KeyValuePair<String, String> s in tag.Field.Filled.ConditionsDictionary)
                    {
                        Console.WriteLine(s + "'");
                    }
                }
                if (tag.Block != null)
                {
                    Console.Write(tag.Id + ": ");
                    Console.WriteLine(tag.Block.Prompt.Message + "'");
                }
            }
        }

        private void CreateGrammarXML(String[] tableItems, String tableName)
        {
            XNamespace xNamespace = XNamespace.Get("http://www.w3.org/2001/06/grammar");
            XElement[] items = new XElement[tableItems.Length];
            for (int i = 0; i < tableItems.Length; i++)
            {
                items[i] = new XElement(xNamespace + "item", tableItems[i]);
            }

            XElement oneOf = new XElement(xNamespace + "one-of", items);
            XElement rule = new XElement(xNamespace + "rule", oneOf);
            rule.SetAttributeValue("id", "rootRule");
            XElement grammar = new XElement(xNamespace + "grammar",
                new XAttribute("version", "1.0"),
                new XAttribute(XNamespace.Xml + "lang", "pl-PL"),
                new XAttribute("root", "rootRule"),
                rule);
            XDocument grammarXML = new XDocument(grammar);

            grammar.Save("C:\\Users\\TwoFa\\source\\repos\\MSP-PoC\\ASR\\XmlFiles\\" + tableName + ".xml");
        }

        private void QueryDatabaseToTable()
        {          
            Dictionary<String, String> FileQueryDictionary = new Dictionary<string, string>();
           
            string AccountNameQuery = "Select * from AccountName";
            string DatesQuery = "Select * from Dates";
            string MovieQuery = "Select * from Movie";
            FileQueryDictionary.Add("Names", AccountNameQuery);
            FileQueryDictionary.Add("Dates", DatesQuery);
            FileQueryDictionary.Add("Movies", MovieQuery);

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            List<String> tableContent = new List<string>();
            foreach (KeyValuePair<string, string> queryFile in FileQueryDictionary)
            {
                sqlConnection.Open();
                cmd.CommandText = queryFile.Value;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tableContent.Add((string)reader[1]);
                }
                CreateGrammarXML(tableContent.ToArray(), queryFile.Key);
                tableContent.Clear();
                sqlConnection.Close();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CurrentForm = FormList[0];
            NextForm = FormList[0];
            FormInterpretationAlgorithm(CurrentForm);
        }

        private void choicesList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void topLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
