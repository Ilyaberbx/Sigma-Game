using System.Threading;
using System.Threading.Tasks;

namespace Odumbrata.Core.Installers
{
    public interface IInstaller
    {
        Task Install(CancellationToken token);

        Task Uninstall();
    }
}