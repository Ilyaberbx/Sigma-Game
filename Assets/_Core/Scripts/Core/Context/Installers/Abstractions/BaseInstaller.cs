using System;
using System.Threading;
using System.Threading.Tasks;
using Better.Commons.Runtime.Extensions;
using Better.Locators.Runtime;
using Better.Services.Runtime.Interfaces;
using UnityEngine;

namespace Odumbrata.Core.Installers
{
    [Serializable]
    public abstract class BaseInstaller<TService> : IInstaller where TService : IService
    {
        [SerializeField] private bool _isParallelInstallation;

        protected abstract TService[] Services { get; }

        public async Task Install(CancellationToken token)
        {
            foreach (var service in Services)
            {
                ServiceLocator.Register(service);

                if (_isParallelInstallation)
                {
                    await service.InitializeAsync(token);
                }
                else
                {
                    service.InitializeAsync(token).Forget();
                }
            }

            foreach (var service in Services)
            {
                service.PostInitializeAsync(token).Forget();
            }
        }

        public Task Uninstall()
        {
            foreach (var service in Services)
            {
                if (service is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                ServiceLocator.Unregister(service);
            }

            return Task.CompletedTask;
        }
    }
}