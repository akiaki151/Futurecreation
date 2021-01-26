using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1 : MonoBehaviour
{
    IEnumerator Start()
    {
        Timer.endLoadScene = Time.realtimeSinceStartup;

        Timer.LoadTime = Timer.endLoadScene - Timer.startLoadScene;
        Debug.Log($"startLoadSceneは{Timer.startLoadScene}ミリ秒です。 ");
        Debug.Log($"endLoadSceneは{Timer.endLoadScene}ミリ秒です。 ");
        Debug.Log($"Scene読み込み時間は{Timer.LoadTime}ミリ秒です。 ");


        yield return new WaitForSeconds(1);
    }
}
