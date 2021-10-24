using Germes.Abstractions.Models;

namespace Germes.Abstractions.Services
{
    public interface ICommandParser
    {
        bool Contains(string message);
        CommandItems Parse(string message);
    }
}
