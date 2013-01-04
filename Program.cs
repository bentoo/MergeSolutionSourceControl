using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MergeSourceControl
{
    class Program
    {
        public class SourceInfo
        {
            public string Num { get; set; }
            public List<string> Info { get; set; }
        }

        static void Main(string[] args)
        {
            StringBuilder result = new StringBuilder();
            Dictionary<string, SourceInfo> productValues = new Dictionary<string, SourceInfo>();

            string product = File.ReadAllText(@"solution1.sln");
            string [] parts = product.Replace("\r",string.Empty).Split(new char[] { '\n' });

            int i = 0;
            while (i < parts.Length)
            {
                parts[i] = parts[i].Trim();
                if (parts[i].Contains("SccProjectUniqueName"))
                {
                    string[] uniqueName = parts[i].Split(new char[] { '=' });
                    List<string> info = new List<string>();
                    i++;
                    while (i < parts.Length && !parts[i].Contains("SccProjectUniqueName"))
                    {
                        info.Add(parts[i].Trim());
                        i++;
                    }

                    productValues[uniqueName[1].Trim()] = new SourceInfo { 
                        Num = uniqueName[0].Replace("SccProjectUniqueName",string.Empty).Trim(),
                        Info = info
                    };
                }
                else
                {
                    i++;
                }
            }
            string platform = File.ReadAllText(@"solution2.sln");
            parts = platform.Replace("\r", string.Empty).Split(new char[] { '\n' });
            i = 0;
            while (i < parts.Length)
            {
                parts[i] = parts[i].Trim();
               
                if (parts[i].Contains("SccProjectUniqueName"))
                { 
                    result.AppendLine("\t\t"+parts[i]);
                    
                    string[] uniqueName = parts[i].Split(new char[] { '=' });
                    string name = uniqueName[1].Trim();
                    string Num = uniqueName[0].Replace("SccProjectUniqueName", string.Empty);
                    SourceInfo info = null;
                    if (productValues.TryGetValue(name, out info))
                    {
                        i++;
                        foreach (string line in info.Info)
                        {
                            result.AppendLine("\t\t" + line.Replace(info.Num, Num).Trim());
                        }
                        while (i < parts.Length && !parts[i].Contains("SccProjectUniqueName"))
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i++;
                        while (i < parts.Length && !parts[i].Contains("SccProjectUniqueName"))
                        {
                            result.AppendLine("\t\t" + parts[i].Trim());
                            i++;
                        }
                    }
                }
                else
                {
                    i++;
                }
            }

            File.WriteAllText("source.txt", result.ToString());
        }
        
    }

    
}
