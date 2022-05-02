using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniScreen.View
{
    public abstract class ScreenAnimationBase : MonoBehaviour
    {
        protected bool IsShow = false;

        public virtual UniTask Show(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public virtual UniTask Hide(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}