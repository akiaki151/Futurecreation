using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMouseContllore : MonoBehaviour
{
    Vector3 position;
    GameObject m_NowObject;
    public GameObject SetGameObject;
    public GameObject SetSaveObject;
    public GameObject SetLoadObject;
    public GameObject SetConfigObject;
    // Start is called before the first frame update
    void Start()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = -1.0f;
        this.transform.position = position;
        m_NowObject = SetGameObject;
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

        if (Tag == "Title")
            if (Input.GetMouseButtonDown(0))
                SceneManager.LoadSceneAsync("TitleScene");

        if (Tag == "InGameSave")
            if (Input.GetMouseButtonDown(0))
            {
                m_NowObject.SetActive(false);
                SetSaveObject.SetActive(true);
                m_NowObject = SetSaveObject;
            }

        if (Tag == "InGameLoad")
            if (Input.GetMouseButtonDown(0))
            {
                m_NowObject.SetActive(false);
                SetLoadObject.SetActive(true);
                m_NowObject = SetLoadObject;
            }

        if (Tag == "InGameConfig")
            if (Input.GetMouseButtonDown(0))
            {
                m_NowObject.SetActive(false);
                SetConfigObject.SetActive(true);
                m_NowObject = SetConfigObject;
            }

        if (Tag == "Return")
            if (Input.GetMouseButtonDown(0))
            {
                m_NowObject.SetActive(false);
                SetGameObject.SetActive(true);
                m_NowObject = SetGameObject;
            }
    }
}
