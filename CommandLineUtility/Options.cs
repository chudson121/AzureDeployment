using CommandLine;
using CommandLine.Text;

namespace CommandLineUtility
{
    public class CommandArgumentOptions
    {
        
        // Omitting long name, default --verbose
        [Option("v", HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('d', "debug", Required = false, HelpText = "Turns debugging mode on.")]
        public bool IsDebug { get; set; }

        string _sitename;
        [Option('s', "SiteName", Required = false, HelpText = "Site to operate on")]
        public string SiteName {
            get { return _sitename;}
            set {
                if (value == null)
                    throw new System.ArgumentNullException(nameof(value), "SiteName has a minimum length of 1.");
                if (value.Length == 0)
                    throw new System.ArgumentOutOfRangeException(nameof(value), "SiteName has a minimum length of 1.");
                if (value.Length > 250)
                    throw new System.ArgumentOutOfRangeException(nameof(value), "SiteName has a maximum length of 250.");

                _sitename = value.Trim();
            }
        }

        string _action;
        [Option('a', "Action", Required = true, HelpText = "Action to perform")]
        public string Action
        {
            get { return _action; }
            set {
                if (value == null)
                    throw new System.ArgumentNullException(nameof(value), "Action has a minimum length of 1.");
                if (value.Length == 0)
                    throw new System.ArgumentOutOfRangeException(nameof(value), "Action has a minimum length of 1.");
                if (value.Length > 250)
                    throw new System.ArgumentOutOfRangeException(nameof(value), "Action has a maximum length of 250.");
                
                _action = value.Trim();
            }
        }

        /*
                

                [Option('c', "Child", Required = true, HelpText = "Child File + Path to use.")]
                public string ChildFilePath { get; set; }

                [Option('o', "OutputFile", Required = true, HelpText = "output file")]
                public string OutFilePath { get; set; }
        */

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            
            var help = new HelpText
            {
                Heading = new HeadingInfo(
                    $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} Version: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}"),
                Copyright = new CopyrightInfo(" ", System.DateTime.Now.Year),
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            help.AddPreOptionsLine(
                $"Usage: {System.Reflection.Assembly.GetExecutingAssembly().GetName().Name} site action");
            help.AddOptions(this);
            return help;
          
        }
    }
}
