using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.Extension;
using UnityEngine;
using UnityEngine.Playables;

namespace UniScreen.View
{
    public sealed class TimelineScreenAnimation : ScreenAnimationBase
    {
        [SerializeField] private PlayableDirector _showDirector = default;
        [SerializeField] private PlayableDirector _hideDirector = default;

        public override async UniTask Show(CancellationToken token)
        {
            if (IsShow) return;
            await _showDirector.PlayAsync(token);
            IsShow = true;
        }

        public override async UniTask Hide(CancellationToken token)
        {
            if (!IsShow) return;
            await _hideDirector.PlayAsync(token);
            IsShow = false;
        }
    }
}