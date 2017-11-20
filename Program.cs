using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace EdhHealthCheckTls12
{
    public class Program
    {
        public const string Url = "https://everydayhero.com/health";

        // The most old-fashioned, long-winded way possible (there are much easier ways).
        // Should be compatible with every version of .NET shipped in the last 10 years.
        public static void Main()
        {
            var request = WebRequest.Create(new Uri(Url));

            if (request.GetResponse() is HttpWebResponse response)
            {
                var stream = response.GetResponseStream();

                if (stream != null)
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var text = reader.ReadToEnd();
                        // Could just dump here, but here's a "pretty printed" version...
                        dynamic obj = JsonConvert.DeserializeObject(text);
                        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                        Console.WriteLine(json);
                    }
                }
            }

            Console.WriteLine("Press the ENTER key to exit.");
            Console.ReadLine();
        }
    }
}
