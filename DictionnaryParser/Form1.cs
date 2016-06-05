using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;
using System.Windows.Forms;



namespace DictionnaryParser
{
    public partial class Form1 : Form
    {
        public Dictionary<string, List<Definition>> dictionary { get; set; }

        public Form1()
        {
            this.dictionary = new Dictionary<string, List<Definition>>();
            InitializeComponent();
        }

        
        private void Test(object sender, EventArgs e)
        {
            //var fileContent = File.ReadAllText("Samples\\adj.xml");

            //var doc = new XmlDocument();
            //doc.LoadXml(fileContent);
            //var elements = doc.GetElementsByTagName("WordDefinition");
            //if (elements.Count > 1)
            //{
            //    var parser = new ParseyMcParseface(elements[1].InnerText);
            //    var result = parser.Parse();
            //}
            var input = "n 1: the act of applying force to propel something; \"after reaching the desired velocity the drive is cut off\" [syn: {thrust}, {driving force}]";
            var foundMatch = Regex.Match(input, @"^(?<matchType>adv|adj|v|n){0,1}[ ]*(\d*): (?<definition>[\w\s;"",\.\(\)]+)(?<extraInfo>(?:\[){0,1}(?<eiType>syn|ant): (?:\{(?<exWord>[\w\s]+)\}(?:, ){0,1})*(?:\]){0,1}){0,1}");
            var foundMatches = Regex.Matches(input, @"^(?<matchType>adv|adj|v|n){0,1}[ ]*(\d*): (?<definition>[\w\s;"",\.\(\)]+)(?<extraInfo>(?:\[){0,1}(?<eiType>syn|ant): (?:\{(?<exWord>[\w\s]+)\}(?:, ){0,1})*(?:\]){0,1}){0,1}");

            dictionary.Add("test",new List<Definition>());

            var def = new Definition();
            
            var extraWords = new List<string>();

            if (foundMatch.Groups["matchType"].Success)
            {
                var matchType = foundMatch.Groups["matchType"];
                def.WordType = DefinitionTypeToEnum(matchType.Value);
            }

            if (foundMatch.Groups["definition"].Success)
            {
                var definition = foundMatch.Groups["definition"];
                def.DefinitionText = definition.Value;
            }

            

            if (foundMatch.Groups["extraInfo"].Success)
            {

                Group extraInfo = foundMatch.Groups["extraInfo"];
                
                
                if (foundMatch.Groups["eiType"].Success)
                {
                    Group eiType = foundMatch.Groups["eiType"];

                    if (foundMatch.Groups["exWord"].Success)
                    {
                        switch (eiType.Value)
                        {
                            case "syn":
                                foreach (Capture capture in foundMatch.Groups["exWord"].Captures)
                                {
                                    def.Synonymes.Add(capture.Value);
                                }
                                break;
                            case "ant":
                                foreach (Capture capture in foundMatch.Groups["exWord"].Captures)
                                {
                                    def.Anotnymes.Add(capture.Value);
                                }
                                break;
                        }
                    }
                }
            }


        }

        private WordTypes DefinitionTypeToEnum(string input)
        {
            switch (input)
            {
                case "adj":
                    return WordTypes.Adjective;
                case "adv":
                    return WordTypes.Adverb;
                case "n":
                    return WordTypes.Noun;
                case "v":
                    return WordTypes.Verb;
                default:
                    return WordTypes.Unknown;
            }
        }
    }

    public enum WordTypes
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Unknown
    }

    public class Definition
    {
        public Definition()
        {
            Synonymes = new List<string>();
            Anotnymes = new List<string>();
        }
        public WordTypes WordType { get; set; }
        public string DefinitionText { get; set; }
        public List<string> Synonymes { get; set; }
        public List<string> Anotnymes { get; set; }

    }
}
