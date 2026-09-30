using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Infrastructure.AI
{
    public sealed class OllamaOptions
    {
        public const string SectionName = "Ollama";

        public string Endpoint { get; init; } = "http://localhost:11434";

        public string Model { get; init; } = "llama3.2";

        public int TimeoutSeconds { get; init; } = 180;
    }
}
