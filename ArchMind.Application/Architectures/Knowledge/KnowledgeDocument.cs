using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Application.Architectures.Knowledge
{
    public sealed class KnowledgeDocument
    {
        public required string Id { get; init; }

        public required string Title { get; init; }

        public required string Category { get; init; }

        public required string Source { get; init; }

        public required string Content { get; init; }
    }
}
