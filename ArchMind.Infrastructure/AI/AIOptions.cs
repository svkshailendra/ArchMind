using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Infrastructure.AI
{
    public sealed class AIOptions
    {
        public const string SectionName = "AI";

        public string Provider { get; init; } = "Ollama";
    }
}
