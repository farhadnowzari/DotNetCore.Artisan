using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetCore.Artisan
{
    public class TerminalCommands
    {
        public IEnumerable<Type> Commands { get; private set; }
        public TerminalCommands()
        {
            Commands = new HashSet<Type>();
        }

        public TerminalCommands AddCommand<TCommand>()
            where TCommand : ITerminalCommand
        {
            Commands = Commands.Append(typeof(TCommand));
            return this;
        }

        public static TerminalCommands BuildInstance()
        {
            return new TerminalCommands();
        }
    }
}