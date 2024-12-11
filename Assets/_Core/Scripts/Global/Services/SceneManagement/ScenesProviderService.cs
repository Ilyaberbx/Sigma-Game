using System;
using System.Linq;
using Better.Services.Runtime;

namespace Odumbrata.Global.Services.SceneManagement
{
    public sealed class ScenesProviderService : PocoService<ScenesProviderServiceSettings>
    {
        public bool TryGetGroup(Type type, out ScenesGroup group)
        {
            group = null;

            var data = Settings.GroupsMap.FirstOrDefault(temp => temp.StateType.Type == type);

            if (data == null)
            {
                return false;
            }

            group = data.Group;
            return true;
        }
    }
}