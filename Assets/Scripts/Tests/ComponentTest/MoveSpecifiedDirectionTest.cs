using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine;

namespace HoloAppTestSample.Test
{
    public class MoveSpecifiedDirectionTest
    {
        class MoveUpDirection_TestScenario : MoveSpecifiedDirection, IMonoBehaviourTest
        {
            public bool IsTestFinished { get; private set; }

            private void Start()
            {
                StartCoroutine(TestScenario());
            }

            private IEnumerator TestScenario()
            {
                // 移動方向を「上」に指定
                Direction = DirectionType.Up;
                // 1フレーム待機
                yield return null;
                
                // アサーション
                Assert.That(transform.position.x, Is.EqualTo(0));
                Assert.That(transform.position.y, Is.Not.EqualTo(0));
                Assert.That(transform.position.z, Is.EqualTo(0));

                // ここまで到達したらテストは成功
                IsTestFinished = true;

                // 他のテストを実行中にもこのMonoBehaviourが動いてしまうので止める
                gameObject.SetActive(false);
            }
        }

        [UnityTest]
        public IEnumerator ObjectMoveUpDirection()
        {
            yield return new MonoBehaviourTest<MoveUpDirection_TestScenario>();
        }
    }
}
