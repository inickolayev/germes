using System.Collections.Generic;
using System.Linq;
using Germes.Abstractions.Services;

namespace Germes.Implementations.Services
{
    public class SourceAdapterFactory : ISourceAdapterFactory
    {
        private readonly IEnumerable<ISourceAdapter> _sourceAdapters;

        public SourceAdapterFactory(IEnumerable<ISourceAdapter> sourceAdapters)
        {
            _sourceAdapters = sourceAdapters;
        }

        public ISourceAdapter GetAdapter(string sourceId)
            => _sourceAdapters.FirstOrDefault(sa => sa.Check(sourceId));
    }
}