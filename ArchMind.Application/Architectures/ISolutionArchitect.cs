using ArchMind.Domain.Architectures; 

namespace ArchMind.Application.Architectures
{
    public interface ISolutionArchitect
    {
        Task<ArchitectureProposal> GenerateAsync(
        ArchitectureRequest request,
        CancellationToken cancellationToken = default);
    }
}
