using System;
using System.Windows.Forms;
using DictionnaryParser.DictServiceRef;

namespace DictionnaryParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetDefinitionButton_Click(object sender, EventArgs e)
        {
            var client = new DictServiceSoapClient("DictServiceSoap");
            var wordDefinition = client.DefineInDict("wn", wordTxtBox.Text);
            if (wordDefinition.Definitions.Length > 0)
            {
                var definitionText = wordDefinition.Definitions[0].WordDefinition;
                ParseyMcParseface parseyMcParseface = new ParseyMcParseface(definitionText);
                var parsedDefinitionText = parseyMcParseface.Parse();
                foreach (var r in parsedDefinitionText)
                {
                    logTextBox.AppendText(r.type + Environment.NewLine);
                    foreach (var def in r.Def)
                    {
                        logTextBox.AppendText("\t" + def.text + Environment.NewLine);
                        foreach (var syn in def.synonym)
                        {
                            logTextBox.AppendText("\t\t" + syn + Environment.NewLine);
                        }
                    }
                }
            }
        }
    }
}
