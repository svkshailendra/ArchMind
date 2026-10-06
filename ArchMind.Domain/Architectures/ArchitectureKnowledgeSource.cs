using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Domain.Architectures
{
    public sealed class ArchitectureKnowledgeSource
    {
        public required string Id { get; init; }

        public required string Title { get; init; }

        public required string Category { get; init; }

        public required string Reason { get; init; }
    }
}
