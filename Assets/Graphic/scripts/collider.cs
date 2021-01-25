using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{

    private float time;
    public int i;
    private TMPAnimation _tmp;

    // Start is called before the first frame update
    void Start()
    {
        time =0;

        foreach (Transform child in this.transform)
        {
            _tmp = child.gameObject.GetComponent<TMPAnimation>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnMouseEnter()
    {
       
    }
    private void OnMouseOver()
    {
        time += 0.01f;
        _tmp.speed = time;
        _tmp.SetAnim();
    }

    void OnMouseExit()
    {
        time = 0.0f;
        _tmp.speed = time;
        _tmp.SetAnim();
    }
}
