using UniScreen.Container;
using UnityEngine;

namespace UniScreen.Sample.Scripts
{
    public sealed class Sample : MonoBehaviour
    {
        private static ScreenContainer _screenContainer = default;
        public static ScreenContainer ScreenContainer => _screenContainer;

        private void Awake()
        {
            _screenContainer = new ScreenContainer(new SampleFactory(), transform);
        }

        private async void Start()
        {
            await _screenContainer.NewScreen("Background");
            await _screenContainer.Push("Screen1", true);
        }
    }
}