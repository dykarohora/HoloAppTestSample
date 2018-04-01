using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.EventSystems;

namespace HoloAppTestSample.Test
{

    public class TapToMoveDownTest
    {
        [UnityTest]
        public IEnumerator TappedObject_MoveDown()
        {
            // Use the Assert class to test conditions.
            // yield to skip a frame
            yield return new MonoBehaviourTest<TappedObject_MoveDown_TestScenario>();
        }

        private class TappedObject_MoveDown_TestScenario : TapToMoveDown, IMonoBehaviourTest
        {
            public bool IsTestFinished { get; private set; }

            private bool _isTestStarted = false;

            private void Update()
            {
                if(_isTestStarted) return;

                StartCoroutine(TestScenario());
                _isTestStarted = true;
            }

            private IEnumerator TestScenario()
            {
                yield return null;

                Assert.That(transform.position, Is.EqualTo(Vector3.zero));

                yield return null;

                var dummyData = new InputClickedEventData(EventSystem.current);
                dummyData.Initialize(
                    inputSource: null,
                    sourceId: 0,
                    tag: null,
                    pressType: InteractionSourcePressInfo.None,
                    tapCount: 1
                    );

                OnInputClicked(dummyData);

                yield return new WaitForSeconds(1f);

                Assert.That(transform.position.x, Is.EqualTo(0));
                Assert.That(transform.position.y, Is.Not.EqualTo(0));
                Assert.That(transform.position.z, Is.EqualTo(0));

                IsTestFinished = true;

                gameObject.SetActive(false);
            }
        }
    }
}
