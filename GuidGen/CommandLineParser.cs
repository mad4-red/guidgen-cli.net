using System.Linq;

namespace GuidGen
{
    internal class CommandLineParser
    {
        public CommandLineOptions Parse(string[] args)
        {
            if (args == null || !args.Any())
            {
                return CommandLineOptions.Default;
            }

            var options = new CommandLineOptions();

            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                string key;
                string value = null;

                if (arg.Contains('='))
                {
                    var split = arg.Split('=');
                    key = split[0];
                    value = split[1];
                }
                else if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                {
                    key = args[i];
                    value = args[i + 1];
                    i++;
                }
                else
                {
                    key = args[i];
                }

                if (key == "-h" || key == "--help")
                {
                    options.Help = true;
                    break;
                }
                if (key == "-v" || key == "--version")
                {
                    options.Version = true;
                    break;
                }

                if (key == "-u" || key == "--upper")
                {
                    options.Upper = true;
                    continue;
                }

                if (key == "-f" || key == "--format")
                {
                    options.Format = value;
                    continue;
                }

                if (key == "-n" || key == "--number")
                {
                    int o;
                    if (!int.TryParse(value, out o) || o <= 0)
                    {
                        throw new InvalidOptionException(key, "Argument to guidgen must be an integer greater than 0");
                    }
                    options.Number = o;
                    continue;
                }

                if (key == "--clip")
                {
                    options.Clipboard = true;
                    continue;
                }

                throw new UnknownOptionException(key);
            }

            return options;
        }
    }
}
