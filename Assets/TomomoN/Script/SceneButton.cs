using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    public void OnClickSceneButton()
    {
        Debug.Log("シーン遷移");
        SceneManager.LoadScene("Text");
    }
}
