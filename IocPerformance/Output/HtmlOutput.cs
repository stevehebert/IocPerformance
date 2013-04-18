using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IocPerformance.Output
{
    public class HtmlOutput : IOutput
    {
        private readonly List<Result> results = new List<Result>();

        public void Start()
        {
        }

        public void Result(Result result)
        {
            this.results.Add(result);
        }

        public void Finish()
        {
            if (!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }

            using (var fileStream = new FileStream("output\\result.txt", FileMode.Create))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("<tr><th>Container</th><th>Singleton</th><th>Transient</th><th>Combined</th><th>Interception</th></tr>");

                    foreach (var result in this.results)
                    {
                        writer.WriteLine(
                            "<tr><th>{0}{1}{2}</th><t{3}>{4}</t{3}><t{5}>{6}</t{5}><t{7}>{8}</t{7}><t{9}>{10}</t{9}></tr>",
                            result.Name,
                            result.Version == null ? string.Empty : " ",
                            result.Version);
                    }
                }
            }
        }
    }
}
