using HoloToolkit.Unity.InputModule;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HoloAppTestSample
{

    public class ManipulateObject : MonoBehaviour, IManipulationHandler
    {
        private bool _isManipulating;
        [SerializeField] [Range(1.0f, 10.0f)] private float _multipler = 5.0f;

        // Use this for initialization
        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => _isManipulating)
                .Subscribe(_ => transform.position += _smoothVelocity * _multipler);
        }

        // 以下のコードと透過
        /*
        private void Update()
        {
            if(!_isManipulating) return;

            transform.position += _smoothVelocity * _multipler;
        }
        */

        private Vector3 _lastNavigatePos = Vector3.zero;
        private Vector3 _navigateVelocity = Vector3.zero;

        // 前フレームからの手の位置の移動ベクトル
        private Vector3 _smoothVelocity = Vector3.zero;

        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            _isManipulating = true;
            InputManager.Instance.PushModalInputHandler(gameObject);
        }

        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            var eventPos = eventData.CumulativeDelta;

            _navigateVelocity = eventPos - _lastNavigatePos;
            _lastNavigatePos = eventPos;
            _smoothVelocity = Vector3.Lerp(_smoothVelocity, _navigateVelocity, 0.5f);
        }

        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            _isManipulating = false;
            ResetVectors();
            InputManager.Instance.PopModalInputHandler();
        }

        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            _isManipulating = false;
            ResetVectors();
            InputManager.Instance.PopModalInputHandler();
        }

        private void ResetVectors()
        {
            _lastNavigatePos = Vector3.zero;
            _navigateVelocity = Vector3.zero;
            _smoothVelocity = Vector3.zero;
        }
    }

}
