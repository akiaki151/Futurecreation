using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    GameObject mouseeffect;

    private Vector3 mousePosition;
    private Vector3 mouseBeforePosition;
 
    public GameObject particle;             //追尾パーティクル
    public GameObject tapParticle;          //タップパーティクル

    private ParticleSystem partiSystem;
    private ParticleSystem tapPartiSystem;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        particle = (GameObject)Instantiate(particle);
        tapParticle = (GameObject)Instantiate(tapParticle);

        partiSystem = particle.GetComponent<ParticleSystem>();
        tapPartiSystem = tapParticle.GetComponent<ParticleSystem>();

        partiSystem.Stop();
        tapPartiSystem.Stop();
        mouseBeforePosition = mousePosition;
    }

    // Update is called once per frame
    void Update()
    {

        time += 1*Time.deltaTime;

        if(time>0.01)
        {
            time = 0;
            mouseBeforePosition = mousePosition;
        }

        //particle.Play();
        MouseController();
    }
    
    void MouseController()
    {

        mousePosition = Input.mousePosition;
        mousePosition.z = 10.0f;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        partiSystem.transform.position = mousePosition;
        partiSystem.Play();

         if (Mathf.Abs(mousePosition.x - mouseBeforePosition.x) < 0.01f || Mathf.Abs(mousePosition.y - mouseBeforePosition.y) < 0.01f)
         {
             partiSystem.Stop();
         }

        //Debug.Log(mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            tapPartiSystem.transform.position = mousePosition;
            tapPartiSystem.Play();

        }
        
    }

}
