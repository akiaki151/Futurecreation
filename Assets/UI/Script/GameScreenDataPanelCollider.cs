using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenDataPanelCollider : MonoBehaviour
{
    [SerializeField] private GameObject DataPanel;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        box = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Vector3 temp = DataPanel.transform.localPosition;

        temp.x -= 200;

        DataPanel.transform.localPosition = temp;
    }

    private void OnMouseExit()
    {
        Vector3 temp = DataPanel.transform.localPosition;

        temp.x += 200;

        DataPanel.transform.localPosition = temp;
    }
}
