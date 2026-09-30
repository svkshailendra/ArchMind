using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Domain.Architectures
{
    public sealed class ArchitectureRelationship
    {
        public string From { get; set; } = string.Empty;

        public string To { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
    }
}
