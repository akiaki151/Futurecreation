using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public Camera MainCamera;
    public Transform ButtonPanel;
    public Button OptionButton;
    public TextMeshProUGUI TMP;
    public Text Text;
    public Text Speaker;
    public GameObject Delta;

    private void Start()
    {
        Delta.transform.DOMoveY(-0.2f, 1.0f).SetRelative().SetEase(Ease.InCubic)
            .SetLoops(-1, LoopType.Restart);
    }
}