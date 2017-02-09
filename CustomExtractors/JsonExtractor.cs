using Microsoft.Analytics.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CustomExtractors
{
    public class User
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
    }

    public class Log
    {
        public int deviceid { get; set; }
        public string build { get; set; }
        public User user { get; set; }
    }

    public class JsonExtractor : IExtractor
    {
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            using (StreamReader reader = new StreamReader(input.BaseStream))
            {
                string jsonString = reader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    yield return output.AsReadOnly();
                }

                Log log = JsonConvert.DeserializeObject<Log>(jsonString);

                if (log == null)
                {
                    yield return output.AsReadOnly();
                }

                output.Set("DeviceId", log.deviceid);
                output.Set("Build", log.build);
                output.Set("FirstName", log.user.firstname);
                output.Set("LastName", log.user.lastname);

                yield return output.AsReadOnly();
            }
        }
    }
}