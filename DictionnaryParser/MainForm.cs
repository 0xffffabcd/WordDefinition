using System;
using System.Windows.Forms;

namespace DictionnaryParser
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void GetDefinition_ButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            logTextBox.Clear();
            button.Enabled = false;
            try
            {
                var client = new DictServiceRef.DictServiceSoapClient("DictServiceSoap");
                var wd = client.DefineInDict("wn", wordTxtBox.Text);

                if (wd.Definitions.Length > 0)
                {
                    var result = DefinitionParser.Parse(wd.Definitions[0].WordDefinition);
                    foreach (var definition in result)
                    {
                        logTextBox.AppendText(
                            $"{definition.WordType} : {definition.DefinitionText} {Environment.NewLine}");
                        foreach (var anotnym in definition.Anotnyms)
                        {
                            logTextBox.AppendText($"\tAntonym : {anotnym} {Environment.NewLine}");
                        }

                        foreach (var synonym in definition.Synonyms)
                        {
                            logTextBox.AppendText($"\tSynonym : {synonym} {Environment.NewLine}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No definition found!", "Word not found", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            finally
            {
                button.Enabled = true;
            }

        }
    }
}
