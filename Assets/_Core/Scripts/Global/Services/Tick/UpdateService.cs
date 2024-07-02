using UnityEngine;

namespace Odumbrata.Global.Services
{
    public sealed class UpdateService : BaseTickService<IUpdatable>
    {
        private void Update()
        {
            Tick(Time.deltaTime);
        }
    }
}