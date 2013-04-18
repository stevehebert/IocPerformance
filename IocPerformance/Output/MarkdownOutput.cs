using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IocPerformance.Output
{
    public class MarkdownOutput : IOutput
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
            using (var fileStream = new FileStream("../../../README.md", FileMode.Create))
            {
                using (var writer = new StreamWriter(fileStream))
                {
                    writer.WriteLine("Ioc Performance");
                    writer.WriteLine("===============");
                    writer.WriteLine("");
                    writer.WriteLine("Source code of my performance comparison of the most popular .NET IoC containers:  ");
                    writer.WriteLine("[www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison](http://www.palmmedia.de/Blog/2011/8/30/ioc-container-benchmark-performance-comparison)");
                    writer.WriteLine("");
                    writer.WriteLine("Author: Daniel Palme  ");
                    writer.WriteLine("Blog: [www.palmmedia.de](http://www.palmmedia.de)  ");
                    writer.WriteLine("Twitter: [@danielpalme](http://twitter.com/danielpalme)  ");
                    writer.WriteLine("");
                    writer.WriteLine("Results");
                    writer.WriteLine("-------");
                    writer.WriteLine("<table>");

                    writer.Write("<tr>");
                    foreach (var item in Output.Result.ColumnNames)
                        writer.Write("<th>{0}</th>", item);
                    writer.Write("</tr>");

                    foreach (var result in this.results)
                    {
                        writer.Write("<tr><th>{0}</th></tr>");
                        foreach (var item in result.Results)
                            writer.Write("<td>{0}</td>", item == null?  "n/a" : item.ToString());
                        writer.WriteLine("</tr>");
                    }

                    writer.WriteLine("</table>");
                }
            }
        }
    }
}
