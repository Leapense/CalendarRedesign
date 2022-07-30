using System.Threading.Tasks;

namespace Calendar_Redesign.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}
