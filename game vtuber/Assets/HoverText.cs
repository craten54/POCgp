using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTextEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public TextMeshProUGUI textUI;
    [SerializeField] private string originalText;

    void Start()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textUI.text = ">" + originalText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textUI.text = " " + originalText;
    }
}
