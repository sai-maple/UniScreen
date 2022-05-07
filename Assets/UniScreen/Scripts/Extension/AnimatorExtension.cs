using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniScreen.Extension
{
    public static class AnimatorExtension
    {
        public static async UniTask SetTriggerAsync(this Animator animator, int id, int layer = 0,
            CancellationToken token = default)
        {
            animator.SetTrigger(id);
            await UniTask.DelayFrame(1, cancellationToken: token);
            if (token.IsCancellationRequested) return;
            var state = animator.IsInTransition(layer)
                ? animator.GetNextAnimatorStateInfo(layer)
                : animator.GetCurrentAnimatorStateInfo(layer);
            await UniTask.Delay(TimeSpan.FromSeconds(state.length), cancellationToken: token);
        }
    }
}