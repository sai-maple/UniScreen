using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UniScreen.Sample.Scripts
{
    public sealed class BackScreenButton : MonoBehaviour
    {
        private Button _button = default;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Sample.ScreenContainer.Pop().Forget());
        }
    }
}