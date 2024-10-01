using System.Threading;
using System.Threading.Tasks;

namespace Odumbrata.Services.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public virtual void Initialize()
        {
        }

        public abstract Task Do(CancellationToken cancellationToken);
    }
}