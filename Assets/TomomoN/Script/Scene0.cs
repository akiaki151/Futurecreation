using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene0 : MonoBehaviour
{
    IEnumerator Start()
    {
        // Unityが安定するまで10秒待つ
        yield return new WaitForSeconds(10);

        SceneManager.LoadScene("Heroine001");

        Timer.startLoadScene = Time.realtimeSinceStartup;
    }
}
