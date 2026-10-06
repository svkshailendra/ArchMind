using ArchMind.Domain.Architectures;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Application.Architectures.Knowledge
{
    public interface IArchitectureKnowledgeRetriever
    {
        Task<IReadOnlyList<KnowledgeChunk>> RetrieveAsync(
            ArchitectureRequest request,
            CancellationToken cancellationToken = default);
    }
}
