using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace HoloAppTestSample
{
    /// <summary>
    /// マニピュレーションデータを受け取ってオブジェクトを移動させるコンポーネント
    /// </summary>
    public class MoveByManipulate : MonoBehaviour
    {
        private IManipulationDataProvider _dataProvider;

        [SerializeField] [Range(1.0f, 10.0f)] private float _multipler = 5.0f;

        // Use this for initialization
        private void Start()
        {
            _dataProvider = GetComponent<IManipulationDataProvider>();

            this.UpdateAsObservable()
                .Where(_ => _dataProvider.IsManipulating)
                .Subscribe(_ => transform.position += _dataProvider.SmoothVelocity * 5f);
        }
    }
}
