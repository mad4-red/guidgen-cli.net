using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle("GuidGen")]
[assembly: AssemblyProduct("guidgen-cli.net")]
[assembly: AssemblyCopyright("Copyright © mad4_red 2016")]
[assembly: AssemblyDescription(".NET command line tool to generate GUID")]

[assembly: AssemblyVersion("1.0.0.0")]

#if DEBUG
[assembly: InternalsVisibleTo("GuidGen.Test")]
#endif