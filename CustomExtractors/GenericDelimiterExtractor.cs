using Microsoft.Analytics.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace CustomExtractors
{
    public class GenericDelimiterExtractor : IExtractor
    {
        private char delimiter;

        public GenericDelimiterExtractor(char delimiter)
        {
            this.delimiter = delimiter;
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            using (StreamReader reader = new StreamReader(input.BaseStream))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split(this.delimiter);

                    for (int i = 0; i < tokens.Length; i++)
                    {
                        output.Set(i, tokens[i]);
                    }

                    yield return output.AsReadOnly();
                }
            }
        }
    }
}
