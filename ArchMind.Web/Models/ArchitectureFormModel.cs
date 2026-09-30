using ArchMind.Domain.Architectures;
using System.ComponentModel.DataAnnotations;

namespace ArchMind.Web.Models
{
    public sealed class ArchitectureFormModel
    {
        [Required(ErrorMessage = "Describe the business problem.")]
        [StringLength(
5000,
MinimumLength = 20,
ErrorMessage = "Enter between 20 and 5,000 characters.")]
        public string BusinessProblem { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specify the expected users or scale.")]
        [StringLength(500)]
        public string ExpectedUsers { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specify at least one constraint.")]
        [StringLength(2000)]
        public string Constraints { get; set; } =
        "The solution must use free and open-source services.";

        [StringLength(1000)]
        public string PreferredTechnology { get; set; } =
        ".NET, Blazor, Docker and open-source technologies";

        public ArchitectureRequest ToRequest()
        {
            return new ArchitectureRequest
            {
                BusinessProblem = BusinessProblem.Trim(),
                ExpectedUsers = ExpectedUsers.Trim(),
                Constraints = Constraints.Trim(),
                PreferredTechnology = PreferredTechnology.Trim()
            };
        }
    }
}
