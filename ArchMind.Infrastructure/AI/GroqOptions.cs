using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Infrastructure.AI
{
    public sealed class GroqOptions
    {
        public const string SectionName = "Groq";

        public string Endpoint { get; set; } =
            "https://api.groq.com/openai/v1";

        public string Model { get; set; } =
            "llama-3.3-70b-versatile";

        public int TimeoutSeconds { get; set; } = 180;

        public string ApiKey { get; set; } = string.Empty;
    }
}
