using System.Threading;
using System.Threading.Tasks;

namespace Odumbrata.Services.Commands
{
    public interface ICommand
    {
        public Task Do(CancellationToken cancellationToken);
    }
}