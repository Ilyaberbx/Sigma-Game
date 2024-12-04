using System.Threading;
using System.Threading.Tasks;
using Better.Services.Runtime;
using Odumbrata.Core.Saves;
using Odumbrata.Data.Runtime;
using UnityEngine;

namespace Odumbrata.Global.Services.User
{
    [CreateAssetMenu(fileName = "New User Service Config", menuName = "Configs/Services/User", order = 0)]
    public class UserSettings : ScriptableObject
    {
    }

    public sealed class UserService : PocoService<UserSettings>
    {
        private ISavesSystem _savesSystem;

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            _savesSystem = new PrefsSavesSystem();
            return Task.CompletedTask;
        }

        protected override Task OnPostInitializeAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}