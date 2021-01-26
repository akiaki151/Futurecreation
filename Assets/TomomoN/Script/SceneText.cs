using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneText : MonoBehaviour
{
    IEnumerator Start()
    {
        // Unityが安定するまで10秒待つ
        yield return new WaitForSeconds(1);

        Timer.startLoadScene = Time.realtimeSinceStartup;
    }
}
