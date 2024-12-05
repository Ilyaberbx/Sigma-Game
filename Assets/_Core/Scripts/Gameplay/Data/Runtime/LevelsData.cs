using System;
using Odumbrata.Core.Commons.Observable;

namespace Odumbrata.Data.Runtime
{
    [Serializable]
    public sealed class LevelsData : Observable
    {
        public int Index;
    }
}