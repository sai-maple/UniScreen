using UnityEngine;
using UnityEngine.UI;

namespace UniScreen.Sample.Scripts
{
    public sealed class NewScreenButton : MonoBehaviour
    {
        [SerializeField] private string _screenName = default;
        [SerializeField] private bool isOverride = default;
        private Button _button = default;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(async () =>
            {
                await Sample.ScreenContainer.NewScreen("Background");
                await Sample.ScreenContainer.Push(_screenName, isOverride);
            });
        }
    }
}