using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UniScreen.Sample.Scripts
{
    public sealed class MoveScreenButton : MonoBehaviour
    {
        [SerializeField] private string _screenName = default;
        [SerializeField] private bool isOverride = default;
        private Button _button = default;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Sample.ScreenContainer.Push(_screenName, isOverride).Forget());
        }
    }
}