namespace DotNetCore.Artisan
{
    public interface ITerminalCommand
    {
        string Name { get; }
        void Execute( string[] args );
    }
}