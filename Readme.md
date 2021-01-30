# DotNetCore.Artisan

With this package you will be able to add artisan commands to your dotnet core project and run your own custom commands while you are at production. **Inspired from Laravel**
The commands will have the `IServiceProvider` injected into their constuctor so you can create your scope to have the DbContext and other scoped dependencies.

**Note:** The commands will be added as `Transient`.

## Installation

This is available in nuget package store.

You also can add it with dotnet cli with the following command.
```
dotnet add package DotNetCore.Artisan
```

## Usage

First you need to change your `Program.cs` to make sure that on run it checks for artisan commands. Please change your `Program.cs` to the following:
```
CreateHostBuilder(args).Build().RunWithCommands(args);
```
**Note:** Noticed the change? normally it is `Run()` which will run your app but we first check if we are running with any of the registered commands and if not then it will run normally

For this next step I am used to give the terminal their own Folder so you can create a "Terminal" folder and add a Kernel.cs in there.

`Kernel.cs` file will handle the registeration of your commands into `IServiceCollection` like below:
```
    public static class Kernel
    {
        public static IServiceCollection AddDefaultArtisan(this IServiceCollection services)
        {
            services.AddArtisanTerminal(options => {
                options.AddCommand<YOUR_CUSTOM_COMMAND>();
            });
            return services;
        }
    }
```

The command that your are going to add into your ArtisanCommands must implement the `ITerminalCommand` which will get a name and has an "Execute" method which your main code will be placed there. Also you can use `IServiceProvider` inside your `ITerminalCommand` from the constructor for getting the scoped dependencies or normal Transient dependencies.


I hope you  find this useful and have a nice time programming ;)
