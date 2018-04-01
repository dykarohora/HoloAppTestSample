using HoloToolkit.Unity.InputModule;
using UnityEngine;

namespace HoloAppTestSample
{
    /// <summary>
    /// マニピュレーション中の指の移動ベクトルを提供するコンポーネント
    /// </summary>
    public class ManipulationDataProvider : MonoBehaviour, IManipulationHandler, IManipulationDataProvider
    {
        public bool IsManipulating { get; private set; } = false;
        public Vector3 SmoothVelocity { get; private set; } = Vector3.zero;

        private Vector3 _lastNavigatePos = Vector3.zero;
        private Vector3 _navigateVelocity = Vector3.zero;

        public void OnManipulationStarted(ManipulationEventData eventData)
        {
            IsManipulating = true;
            InputManager.Instance.PushModalInputHandler(gameObject);
        }

        public void OnManipulationUpdated(ManipulationEventData eventData)
        {
            var eventPos = eventData.CumulativeDelta;

            _navigateVelocity = eventPos - _lastNavigatePos;
            _lastNavigatePos = eventPos;
            SmoothVelocity = Vector3.Lerp(SmoothVelocity, _navigateVelocity, 0.5f);
        }

        public void OnManipulationCompleted(ManipulationEventData eventData)
        {
            IsManipulating = false;
            ResetVectors();
            InputManager.Instance.PopModalInputHandler();
        }

        public void OnManipulationCanceled(ManipulationEventData eventData)
        {
            IsManipulating = false;
            ResetVectors();
            InputManager.Instance.PopModalInputHandler();
        }

        private void ResetVectors()
        {
            _lastNavigatePos = Vector3.zero;
            _navigateVelocity = Vector3.zero;
            SmoothVelocity = Vector3.zero;
        }
    }

}
