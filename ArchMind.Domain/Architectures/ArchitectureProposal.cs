using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Domain.Architectures
{
    public sealed record ArchitectureProposal
    {
        public required string Summary { get; init; }

        public required IReadOnlyList<string> Requirements { get; init; }

        public required IReadOnlyList<ArchitectureComponent> Components { get; init; }

        public required IReadOnlyList<ArchitectureRelationship> Relationships { get; init; }

        public required IReadOnlyList<string> SecurityConsiderations { get; init; }

        public required IReadOnlyList<string> ScalabilityConsiderations { get; init; }

        public required IReadOnlyList<string> Risks { get; init; }

        public required IReadOnlyList<string> Assumptions { get; init; }

        public required string MermaidDiagram { get; init; } = string.Empty;
    }

    public sealed record ArchitectureComponent
    {
        public required string Name { get; init; }

        public required string Responsibility { get; init; }

        public required string Technology { get; init; }

        public required string Rationale { get; init; }
    }
}
