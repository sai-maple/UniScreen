using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.View;

namespace UniScreen.Factory
{
    public abstract class ScreenFactory
    {
        public virtual UniTask<ScreenView> CreateAsync(string screen, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}