using System;
using HoloToolkit.Unity.InputModule;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HoloAppTestSample
{
    public class TapToMoveDown : MonoBehaviour, IInputClickHandler
    {
        private bool _isTapped = false;

        public void OnInputClicked(InputClickedEventData eventData)
        {
            _isTapped = true;
        }

        private void Start()
        {
            // AirTapされたらオブジェクトが下に移動する
            this.UpdateAsObservable()
                .Where(_ => _isTapped)
                .Subscribe(_ => transform.position += Vector3.down * 0.01f);
        }
        
        // 以下のコードと投下
        /*
        private void Update()
        {
            if (!_isTapped) return;

            transform.position += Vector3.down * 0.01f;
        }
        */
    }

}
