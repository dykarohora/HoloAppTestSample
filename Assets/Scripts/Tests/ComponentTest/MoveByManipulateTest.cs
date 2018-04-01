using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

namespace HoloAppTestSample.Test
{
    public class MoveByManipulateTest
    {

        [UnityTest]
        public IEnumerator Move_WhenManipulationDataProvide()
        {
            yield return new MonoBehaviourTest<Move_WhenManipulationDataProvide_TestScenario>();
        }

        /// <summary>
        /// ManipulationDataProviderのモックコンポーネント
        /// </summary>
        private class ManipulationDataProviderMock : MonoBehaviour, IManipulationDataProvider
        {

            public bool IsManipulating { get; set; }
            public Vector3 SmoothVelocity { get; set; }
        }

        private class Move_WhenManipulationDataProvide_TestScenario : MoveByManipulate, IMonoBehaviourTest
        {
            public bool IsTestFinished { get; private set; }

            private bool _isTestStarted = false;

            private void Awake()
            {
                // モックコンポーネントをテスト対象のGameObjectにアタッチする
                gameObject.AddComponent<ManipulationDataProviderMock>();
            }

            private void Update()
            {
                if (_isTestStarted) return;

                StartCoroutine(TestScenario());
                _isTestStarted = true;
            }

            private IEnumerator TestScenario()
            {
                // テスト対象からモックコンポーネント
                var dataProvider = gameObject.GetComponent<ManipulationDataProviderMock>();
                Assert.That(dataProvider, Is.Not.Null);

                yield return null;

                // モックコンポーネントを操作し、疑似的にマニピュレーションデータを設定
                dataProvider.IsManipulating = true;
                dataProvider.SmoothVelocity = new Vector3(0, 0, 1f);

                yield return null;

                // オブジェクトが移動していることを確認する
                Assert.That(transform.position.x, Is.EqualTo(0));
                Assert.That(transform.position.y, Is.EqualTo(0));
                Assert.That(transform.position.z, Is.Not.EqualTo(0));

                IsTestFinished = true;

                gameObject.SetActive(false);
            }
        }
    }

}
