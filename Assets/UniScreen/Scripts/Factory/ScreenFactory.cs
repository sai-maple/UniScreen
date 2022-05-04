using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.View;
using UnityEngine;

namespace UniScreen.Factory
{
    public abstract class ScreenFactory
    {
        public virtual UniTask<ScreenView> CreateAsync(string screen, Transform content, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}