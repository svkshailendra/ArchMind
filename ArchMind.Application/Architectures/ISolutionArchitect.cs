using ArchMind.Domain.Architectures;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchMind.Application.Architectures
{
    public interface ISolutionArchitect
    {
        Task<ArchitectureProposal> GenerateAsync(
        ArchitectureRequest request,
        CancellationToken cancellationToken = default);
    }
}
