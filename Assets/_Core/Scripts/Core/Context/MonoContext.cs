using Better.Attributes.Runtime.Select;
using Better.Commons.Runtime.Extensions;
using Odumbrata.Core.Installers;
using UnityEngine;

namespace Odumbrata.Core
{
    public sealed class MonoContext : MonoBehaviour
    {
        [SerializeReference, Select(typeof(IInstaller))]
        private IInstaller[] _installers;

        [SerializeField] private bool _isParallelInstallation;

        private async void Awake()
        {
            foreach (var installer in _installers)
            {
                if (_isParallelInstallation)
                {
                    installer.Install(destroyCancellationToken).Forget();
                }
                else
                {
                    await installer.Install(destroyCancellationToken);
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var installer in _installers)
            {
                installer.Uninstall().Forget();
            }
        }
    }
}