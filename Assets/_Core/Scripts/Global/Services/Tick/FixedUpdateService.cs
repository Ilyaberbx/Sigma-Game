using UnityEngine;

namespace Odumbrata.Global.Services
{
    public sealed class FixedUpdateService : BaseTickService<IFixedUpdatable>
    {
        private void FixedUpdate()
        {
            Tick(Time.fixedDeltaTime);
        }
    }
}