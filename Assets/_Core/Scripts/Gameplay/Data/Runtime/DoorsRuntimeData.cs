using System;
using Odumbrata.Core.Commons.Observable;

namespace Odumbrata.Data.Runtime
{
    [Serializable]
    public sealed class DoorRuntimeData : Observable
    {
        public bool IsOpened { get; set; }
        public bool IsLocked { get; set; }
    }
}