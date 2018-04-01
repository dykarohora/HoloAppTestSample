using UnityEngine;

namespace HoloAppTestSample
{
    /// <summary>
    /// プロパティで示される方向に応じてオブジェクトを移動させるコンポーネント
    /// </summary>
    public class MoveSpecifiedDirection : MonoBehaviour
    {
        public enum DirectionType
        {
            None = 0,
            Up,
            Right,
            Down,
            Left
        }

        public DirectionType Direction { get; set; } = DirectionType.None;

        private void Update()
        {
            var moveAmount = Vector3.zero;
            switch (Direction)
            {
                case DirectionType.Up:
                    moveAmount = Vector3.up * 0.01f;
                    break;
                case DirectionType.Right:
                    moveAmount = Vector3.right * 0.01f;
                    break;
                case DirectionType.Down:
                    moveAmount = Vector3.down * 0.01f;
                    break;
                case DirectionType.Left:
                    moveAmount = Vector3.left * 0.01f;
                    break;
                case DirectionType.None:
                    break;
            }
            transform.position += moveAmount;
        }
    }
}
