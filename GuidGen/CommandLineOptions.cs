using System;

namespace GuidGen
{
    internal class CommandLineOptions : IEquatable<CommandLineOptions>
    {
        public static CommandLineOptions Default { get; private set; }

        static CommandLineOptions()
        {
            Default = new CommandLineOptions();
        }

        public bool Help { get; set; }

        public bool Version { get; set; }

        public bool Upper { get; set; }

        public string Format { get; set; }

        public int Number { get; set; }

        public bool Clipboard { get; set; }

        public CommandLineOptions()
        {
            Help = false;
            Version = false;
            Upper = false;
            Format = "";
            Number = 1;
            Clipboard = false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((CommandLineOptions) obj);
        }

        public bool Equals(CommandLineOptions other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Help.Equals(other.Help) && Version.Equals(other.Version) && Upper.Equals(other.Upper) &&
                   string.Equals(Format, other.Format) && Number == other.Number && Clipboard.Equals(other.Clipboard);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Help.GetHashCode();
                hashCode = (hashCode * 397) ^ Version.GetHashCode();
                hashCode = (hashCode * 397) ^ Upper.GetHashCode();
                hashCode = (hashCode * 397) ^ (Format != null ? Format.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Number;
                hashCode = (hashCode * 397) ^ Clipboard.GetHashCode();
                return hashCode;
            }
        }
    }
}
