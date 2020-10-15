using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace InternetBankingRESTfulService
{
    public  static class CalculateMD5
    {

        static void Run(IEnumerable<string> args)
        {
            args = from arg in args
                   select arg.Trim() into arg
                   where !string.IsNullOrEmpty(arg)
                   select arg;

            if (!args.Any())
                throw new Exception("Missing file path.");

            foreach (var e in from path in args
                              select new
                              {
                                  Path = Path.GetFullPath(path),
                                  Hash = BitConverter.ToString(MD5Hash(path))
                                                    .ToLowerInvariant()
                                                    .Replace("-", string.Empty)
                              })
            {
                Console.WriteLine(e.Hash + " " + e.Path);
            }
        }

        static byte[] MD5Hash(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open,
                                                     FileAccess.Read,
                                                     FileShare.Read,
                                                     4096,
                                                     FileOptions.SequentialScan))
            {
                return MD5.Create().ComputeHash(stream);
            }
        }

        static int Main(string[] args)
        {
            try
            {
                Run(args);
                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Trace.TraceError(e.ToString());
                return Environment.ExitCode != 0 ? Environment.ExitCode : 0xbad;
            }
        }
    }
}
