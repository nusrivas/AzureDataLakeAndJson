using Microsoft.Analytics.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace CustomExtractors
{
    /* input line: firstname|lastname|address|age
    */

    public class PipeExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            using (StreamReader reader = new StreamReader(input.BaseStream))
            {
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split("|".ToCharArray());

                    if (tokens.Length == 4)
                    {
                        output.Set<string>("FirstName", tokens[0]);
                        output.Set<string>("LastName", tokens[1]);
                        output.Set<string>("Address", tokens[2]);
                        output.Set<int>("Age", int.Parse(tokens[3]));
                    }

                    yield return output.AsReadOnly();
                }
            }
        }
    }
}
