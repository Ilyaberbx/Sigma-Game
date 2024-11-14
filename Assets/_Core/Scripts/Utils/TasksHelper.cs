using System.Threading.Tasks;

namespace Odumbrata.Utils
{
    public static class TasksHelper
    {
        public static Task DelayInSeconds(int value)
        {
            return Task.Delay(value * 1000);
        }
    }
}