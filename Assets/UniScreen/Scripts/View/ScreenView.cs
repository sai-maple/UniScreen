using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UniScreen.View
{
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class ScreenView : MonoBehaviour
    {
        [SerializeField] private ScreenAnimationBase[] _screenAnimations = default;
        private readonly Stack<ScreenView> _screens = new Stack<ScreenView>();
        private CanvasGroup _canvasGroup = default;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public ScreenView Create(Transform content)
        {
            return Instantiate(this, content, false);
        }

        public async UniTask HideCurrent(bool isOverride = false, CancellationToken token = default)
        {
            if (!isOverride && _screens.TryPeek(out var current)) await current.HideAll(token);
        }

        public async UniTask PushScreen(ScreenView newScreen, CancellationToken token = default)
        {
            _screens.Push(newScreen);
            await newScreen.ShowAll(token);
        }

        public async UniTask<bool> BackScreen(CancellationToken token = default)
        {
            if (!_screens.TryPop(out var current)) return false;
            var isLastPage = _screens.Count == 0;

            if (isLastPage)
            {
                await UniTask.WhenAll(current.Close(token), Close(token));
            }
            else
            {
                await current.Close(token);
                await _screens.Peek().ShowAll(token);
                Destroy(current);
            }

            return isLastPage;
        }

        public async UniTask Close(CancellationToken token = default)
        {
            await HideAll(token);
            Destroy(gameObject);
        }

        public async UniTask ShowAll(CancellationToken token = default)
        {
            var tasks = new List<UniTask>();
            if (_screens.TryPeek(out var current))
            {
                tasks.Add(current.ShowAll(token));
            }

            tasks.AddRange(Enumerable.Select(_screenAnimations, screenAnimation => screenAnimation.Show(token)));
            await UniTask.WhenAll(tasks);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public async UniTask HideAll(CancellationToken token = default)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            var tasks = new List<UniTask>();
            if (_screens.TryPeek(out var current))
            {
                tasks.Add(current.HideAll(token));
            }

            tasks.AddRange(Enumerable.Select(_screenAnimations, screenAnimation => screenAnimation.Hide(token)));
            await UniTask.WhenAll(tasks);
        }
    }
}