using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Application.Common.Exceptions
{
    public sealed class ArchitectureGenerationException : Exception
    {
        public ArchitectureGenerationException(string message)
        : base(message)
        {
        }

        public ArchitectureGenerationException(
        string message,
        Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
