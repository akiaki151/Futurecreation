using UnityEngine;


public class Score : MonoBehaviour
{
    ScoreObserver<int> scoreValue = new ScoreObserver<int>();
    private GameObject _scorerObject;
    private CanvasGroup _canvasGroup;

    public string Name { get; private set; }

    public void Init(string name)
    {
        this.Name = name;
        _scorerObject = gameObject;
        gameObject.SetActive(false);
        Appear();
        
    }

    public void Appear()
    {
        scoreValue.mChanged += value => Debug.LogFormat("{0}:{1}", Name, value);
        scoreValue.Value = 0;  
    }

    public void ChangeScore(int num)
    {
        scoreValue.Value += num;
        scoreValue.SetValueIfNotEqual(scoreValue.Value);
    }

    public void Destroy()
    {
        Destroy(this);
    }
}
