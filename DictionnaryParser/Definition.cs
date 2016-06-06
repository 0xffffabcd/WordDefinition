using System.Collections.Generic;

namespace DictionnaryParser
{
    public class Definition
    {
        public Definition()
        {
            Synonyms = new List<string>();
            Anotnyms = new List<string>();
        }
        public WordTypes WordType { get; set; }
        public string DefinitionText { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Anotnyms { get; set; }

    }
}