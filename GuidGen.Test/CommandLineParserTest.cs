using Newtonsoft.Json;
using NUnit.Framework;

namespace GuidGen
{
    [TestFixture]
    public class CommandLineParserTest
    {
        [TestCase("--help")]
        [TestCase("-h")]
        public void HelpTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            Assert.IsTrue(options.Help);
        }

        [TestCase("--version")]
        [TestCase("-v")]
        public void VersionTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            Assert.IsTrue(options.Version);
        }

        [TestCase("--upper")]
        [TestCase("-u")]
        public void UpperTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            Assert.IsTrue(options.Upper);
        }

        [TestCase("--format N")]
        [TestCase("-f N")]
        [TestCase("--format=N")]
        [TestCase("-f=N")]
        public void FormatTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            Assert.AreEqual("N", options.Format);
        }

        [TestCase("--number 5")]
        [TestCase("-n 5")]
        [TestCase("--number=5")]
        [TestCase("-n=5")]
        public void NumberTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            Assert.AreEqual(5, options.Number);
        }

        [TestCase("-n=a")]
        [TestCase("-n=0")]
        public void InvalidNumberTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var ex = Assert.Throws<InvalidOptionException>(() => parser.Parse(args));
            Assert.That(ex.Option == "-n");
            Assert.That(ex.Message == "Argument to guidgen must be an integer greater than 0");
        }

        [TestCase("--clip")]
        public void ClipboardTest(string s)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            Assert.IsTrue(options.Clipboard);
        }

        [Test]
        public void UnknownOptionTest()
        {
            var parser = new CommandLineParser();

            var ex = Assert.Throws<UnknownOptionException>(() => parser.Parse(new[]{"--unknown"}));
            Assert.That(ex.Option == "--unknown");
            Assert.That(ex.Message == "Unknown Option: --unknown");
        }

        [Test]
        public void UnknownOptionAndValueTest()
        {
            var parser = new CommandLineParser();

            var ex = Assert.Throws<UnknownOptionException>(() => parser.Parse(new[] { "--formta=N" }));
            Assert.That(ex.Option == "--formta");
            Assert.That(ex.Message == "Unknown Option: --formta");
        }

        [TestCase("--upper -n 10 --clip", "{Help:false,Version:false,Upper:true,Format:'',Number:10,Clipboard:true}")]
        [TestCase("--upper --format=X --number 100", "{Help:false,Version:false,Upper:true,Format:'X',Number:100,Clipboard:false}")]
        [TestCase("-f X -n 100", "{Help:false,Version:false,Upper:false,Format:'X',Number:100,Clipboard:false}")]
        [TestCase("-u -f N -n 5", "{Help:false,Version:false,Upper:true,Format:'N',Number:5,Clipboard:false}")]
        public void MultipleOptionTest(string s, string json)
        {
            var args = s.Split(' ');
            var parser = new CommandLineParser();
            var options = parser.Parse(args);

            var expected = JsonConvert.DeserializeObject<CommandLineOptions>(json);
            Assert.AreEqual(expected, options);
        }
    }
}
