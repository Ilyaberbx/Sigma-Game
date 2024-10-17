using System;
using Better.Commons.Runtime.Utility;

namespace Odumbrata.Utils
{
    public static class ObjectHelper
    {
        public static bool IsNull(object obj)
        {
            if (obj == null)
            {
                DebugUtility.LogException<NullReferenceException>("Object is null");
                return true;
            }

            return false;
        }
    }
}