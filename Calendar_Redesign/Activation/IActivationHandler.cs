using System.Threading.Tasks;

namespace Calendar_Redesign.Activation;

public interface IActivationHandler
{
    bool CanHandle(object args);

    Task HandleAsync(object args);
}
