using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using HoloToolkit.Unity.InputModule;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace HoloAppTestSample.Test
{

    public class ManipulateObjectFuntionTest
    {
        // テストの最初にテスト用に用意したシーンを用意する
        [SetUp]
        public void LoadTestScene()
        {
            SceneManager.LoadScene("manipulationTest");
        }

        [UnityTest]
        public IEnumerator ManipulateObject()
        {
            yield return null;

            var targetObject = GameObject.Find("TestTarget");
            var manipulationDataProvider = targetObject.GetComponent<ManipulationDataProvider>();
            var moveByManipulate = targetObject.GetComponent<MoveByManipulate>();

            Assert.That(targetObject, Is.Not.Null);
            Assert.That(manipulationDataProvider, Is.Not.Null);
            Assert.That(moveByManipulate, Is.Not.Null);

            yield return null;

            // マニピュレーションの開始
            manipulationDataProvider.OnManipulationStarted(null);

            yield return null;

            // マニピュレーションのダミーデータ
            var dummyData = new ManipulationEventData(EventSystem.current);
            dummyData.Initialize(
                inputSource: null,
                sourceId: 0,
                tag: null,
                cumulativeDelta: Vector3.one * 0.1f);

            // マニピュレーションのアップデート
            manipulationDataProvider.OnManipulationUpdated(dummyData);

            yield return null;
            
            // オブジェクトが移動したことを確認する
            Assert.That(targetObject.transform.position, Is.Not.EqualTo(Vector3.zero));

            yield return null;
            
            // マニピュレーションの終了
            manipulationDataProvider.OnManipulationCompleted(null);

            // マニピュレーション終了後のオブジェクトの位置
            var completedPos = targetObject.transform.position;

            // 少し待機
            yield return new WaitForSeconds(1f);

            // マニピュレーション終了後はオブジェクトの位置が変化しないことを確認する
            Assert.That(targetObject.transform.position, Is.EqualTo(completedPos));
        }
    }

}
