namespace Germes.Abstractions.Services
{
    public interface ISourceAdapterFactory
    {
        ISourceAdapter GetAdapter(string sourceId);
    }
}