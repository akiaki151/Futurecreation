using UnityEngine;

public class GameController : MonoBehaviour
{
    public SceneController Sc;

    void Start ()
    {
        Sc = new SceneController(this);
        SetFirstScene();
    }

    void Update()
    {
        Sc.WaitClick();
        Sc.SetComponents();
    }

    void SetFirstScene()
    {
        Sc.SetScene("001");
    }

}
