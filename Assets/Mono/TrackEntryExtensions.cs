using Cysharp.Threading.Tasks;
using Spine;

namespace Mono
{
    public static class TrackEntryExtensions
    {
        public static UniTask Wait(this TrackEntry entry)
        {
            return UniTask.WaitUntil(() => entry.IsComplete);
        }
    }
}