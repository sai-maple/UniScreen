using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.Extension;
using UnityEngine;

namespace UniScreen.View
{
    public sealed class AnimatorScreenAnimation : ScreenAnimationBase
    {
        [SerializeField] private Animator _animator = default;
        private static readonly int ShowHash = Animator.StringToHash("Show");
        private static readonly int HideHash = Animator.StringToHash("Hide");


        public override async UniTask Show(CancellationToken token)
        {
            if (IsShow) return;
            await _animator.SetTriggerAsync(ShowHash, token: token);
            IsShow = true;
        }

        public override async UniTask Hide(CancellationToken token)
        {
            if (!IsShow) return;
            await _animator.SetTriggerAsync(HideHash, token: token);
            IsShow = false;
        }
    }
}