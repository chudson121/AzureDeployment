using System;
using Logging.Contracts;

[assembly: CLSCompliant(false)]

namespace CommandLineUtility
{
    /// <summary>
    /// Convert command line args into type safe properties
    /// </summary>
    /// <example>
    ///   var clp = new CommandLineProcessor(args, logger);
    ///   Console.WriteLine(clp.Options.IsDebug);
    /// </example>
    public class CommandLineProcessor : IArgumentCommand
    {
        private readonly ILogger _logger;
        private readonly IArgumentParser _parser;
        private readonly string[] _arguments;

        public CommandArgumentOptions Options { get; private set; }

        public CommandLineProcessor(ILogger logger, IArgumentParser parser, string[] args)
        {
            _logger = logger;
            _logger.Info("Assigning Parser");
            _parser = parser;
            _arguments = args;

            _logger.Info("Validations");
            Validate(_logger, "logger");
            Validate(_parser, "parser");
        }

        public object Execute()
        {
            const string start = "Starting CommandLineProcessor";
            _logger.Info(start);

            return ProcessCommandLine(_arguments);
        }

        private object ProcessCommandLine(string[] args)
        {
            const string commandArgInfo = "Setting command argument options";
            _logger.Info(commandArgInfo);

            if (args != null && args.Length != 0)
            {
                _parser.IsParsable(args);
                //if (CommandLine.Parser.Default.ParseArguments(args, Options))
                //{
                // Values are available here
                //if (options.Verbose)
                //   return string.Format("Debug: {0}", options.IsDebug);
                //}

                _logger.Info("Args found and set");
            }

            _logger.Info("No options set return help");

            //Options.GetUsage();
            //return Options.GetUsage();
            return new {};
        }

        internal void Validate(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }

    public interface IArgumentCommand
    {
        object Execute();
    }

    public interface IArgumentParser
    {
        bool IsParsable(string[] args);
    }

    public class ArgumentParser : IArgumentParser
    {
        private readonly CommandArgumentOptions _options;

        public ArgumentParser()
        {
            _options = new CommandArgumentOptions();
        }

        public bool IsParsable(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments(args, _options);
        }
    }
}