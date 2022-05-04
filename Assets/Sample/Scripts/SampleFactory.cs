using System.Threading;
using Cysharp.Threading.Tasks;
using UniScreen.Factory;
using UniScreen.View;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UniScreen.Sample.Scripts
{
    public sealed class SampleFactory : ScreenFactory
    {
        public override async UniTask<ScreenView> CreateAsync(string screen, Transform content, CancellationToken token)
        {
            var asset = await Addressables.LoadAssetAsync<GameObject>(screen);
            return asset.GetComponent<ScreenView>().Create(content);
        }
    }
}