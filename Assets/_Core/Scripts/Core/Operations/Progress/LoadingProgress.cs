using System;
using Odumbrata.Extensions;

namespace Odumbrata.Core.Operations.Progress
{
    public class LoadingProgress : IProgress<float>
    {
        public event Action<float> OnProgressTicked;
        private const float Ratio = 1F;

        public void Report(float value)
        {
            OnProgressTicked.SafeInvoke(value / Ratio);
        }
    }
}