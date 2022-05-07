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
            AnimatorStateInfo state;
            while (true)
            {
                await UniTask.DelayFrame(1, cancellationToken: token);
                if (token.IsCancellationRequested) return;
                state = animator.GetCurrentAnimatorStateInfo(layer);
                if (state.shortNameHash == id) break;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(state.length), cancellationToken: token);
        }
    }
}