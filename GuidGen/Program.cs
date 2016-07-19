using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GuidGen
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var parser = new CommandLineParser();
            CommandLineOptions options = null;

            try
            {
                options = parser.Parse(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ShowHelp();
                Environment.Exit(0);
            }

            if (options.Help)
            {
                ShowHelp();
                Environment.Exit(0);
            }

            if (options.Version)
            {
                ShowVersion();
                Environment.Exit(0);
            }

            var outputGuids = GetGuids(options.Upper, options.Format, options.Number);

            if (options.Clipboard)
            {
                var sb = new StringBuilder();
                foreach (var outputGuid in outputGuids)
                {
                    Console.WriteLine(outputGuid);
                    sb.AppendLine(outputGuid);
                }
                Clipboard.SetText(sb.ToString().TrimEnd('\r', '\n'));
            }
            else
            {
                foreach (var outputGuid in outputGuids)
                {
                    Console.WriteLine(outputGuid);
                }
            }
        }

        static void ShowVersion()
        {
            var assmbly = Assembly.GetExecutingAssembly();
            var name = assmbly.GetName();
            Console.Write("{0} version {1}", name.Name, name.Version);
        }

        static void ShowHelp()
        {
            var assmbly = Assembly.GetExecutingAssembly();
            var name = assmbly.GetName();
            var description = (AssemblyDescriptionAttribute)Attribute.GetCustomAttributes(assmbly, typeof(AssemblyDescriptionAttribute)).First();

            Console.WriteLine("{0} ({1})", description.Description, name.Version);
            Console.Write(@"
Usage: newid [OPTIONS]
       newid [ -h | --help | -v | --version ]

Options:
  -h, --help                        Display usage
  -v, --version                     Display version information and quit
  -u, --upper                       Output GUID in upper case
  -f, --format [D/N/P/B/X]          Output GUID according to the provided format specifier
  -n, --number <number>             Number of GUIDs generate
      --clip                        Copy to clipboard
");
        }

        static IEnumerable<string> GetGuids(bool upper, string format, int number)
        {
            foreach (var i in Enumerable.Range(0, number))
            {
                var id = Guid.NewGuid();

                var output = "";
                if (string.IsNullOrWhiteSpace(format))
                {
                    output = id.ToString();
                }
                else
                {
                    try
                    {
                        output = id.ToString(format);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(0);
                    }
                }

                if (upper)
                {
                    output = output.ToUpper();
                }

                yield return output;
            }
        }
    }
}
