using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.Factory;
using UniScreen.View;
using UnityEngine;

namespace UniScreen.Container
{
    public class ScreenContainer
    {
        private readonly Stack<ScreenView> _screen = default;
        private readonly ScreenFactory _factory = default;
        private readonly Transform _content = default;

        public ScreenContainer(ScreenFactory factory, Transform content)
        {
            _screen = new Stack<ScreenView>();
            _factory = factory;
            _content = content;
        }

        public async UniTask NewScreen(string screen, bool isOverride = false, CancellationToken token = default)
        {
            if (!isOverride && _screen.TryPeek(out var current)) await current.HideAll(token);
            var asset = await _factory.CreateAsync(screen, _content, token);
            if (token.IsCancellationRequested) return;
            _screen.Push(asset);
            await asset.ShowAll(token);
        }

        public async UniTask Push(string screen, bool isOverride = false, CancellationToken token = default)
        {
            if (!_screen.TryPeek(out var current)) return;
            await current.HideCurrent(isOverride, token);
            if (token.IsCancellationRequested) return;
            var asset = await _factory.CreateAsync(screen, current.transform, token);
            if (token.IsCancellationRequested) return;
            await current.PushScreen(asset, token);
        }

        public async UniTask Pop(CancellationToken token = default)
        {
            if (!_screen.TryPeek(out var current)) return;
            var isLastPage = await current.BackScreen(token);
            if (!isLastPage) return;
            _screen.Pop();
            if (!_screen.TryPeek(out var previous)) return;
            await previous.ShowAll(token);
        }

        public async UniTask Close(CancellationToken token = default)
        {
            if (!_screen.TryPop(out var current)) return;
            await current.Close(token);
            if (!_screen.TryPeek(out var previous)) return;
            await previous.ShowAll(token);
        }
    }
}