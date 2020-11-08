using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseContlloer : MonoBehaviour
{
    Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = -1.0f;
        this.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0.0f;
        this.transform.position = position;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        string Tag = collision.gameObject.tag;
        if (Tag == "Save")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadSceneAsync("SaveScene");

        if (Tag == "Load")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadSceneAsync("LoadScene");

        if (Tag == "Config")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadSceneAsync("ConfigScene");

        if (Tag == "Title")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadSceneAsync("TitleScene");

        if (Tag == "Game")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadSceneAsync("GameScene");
    }
}
