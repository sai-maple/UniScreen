using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UniScreen.Sample.Scripts
{
    public sealed class CloseScreenButton : MonoBehaviour
    {
        private Button _button = default;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Sample.ScreenContainer.Close().Forget());
        }
    }
}