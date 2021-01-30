using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetCore.Artisan
{
    public static class Kernel
    {
        /// <summary>
        /// Inject your commands into DotNetCore DI.
        /// </summary>
		public static IServiceCollection AddArtisanTerminal(this IServiceCollection services, Action<TerminalCommands> options)
        {
            var terminalOptions = TerminalCommands.BuildInstance();
            options.Invoke(terminalOptions);
            foreach (var command in terminalOptions.Commands)
            {
                services.AddTransient(typeof(ITerminalCommand), command);
            }
            return services;
        }

        /// <summary>
        /// Run an instance of your host on demand based on the given args.
        /// </summary>
		public static void RunWithCommands(this IHost host, string[] args)
        {
            if (args.Length >= 1)
            {
                var terminalServices = host.Services.GetServices<ITerminalCommand>();
                // When the sln file is not available dotnet command doesn't know what to run. So on production we should run dotnet [ProjectName].dll which makes the argument index to shift
                var commandIndex = args[0] == "run" ? 1 : 0;
                try
                {
                    var terminalService = terminalServices.FirstOrDefault(t => t.Name == args[commandIndex]);
                    if (terminalService != null)
                    {
                        var commandArgs = new List<string>();
                        for (var i = commandIndex + 1; i < args.Length; i++)
                        {
                            commandArgs.Add(args[i]);
                        }
                        terminalService.Execute(commandArgs.ToArray());
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return;
                }
            }
            host.Run();
        }
    }
}