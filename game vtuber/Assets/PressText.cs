using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressTextEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject bgObject;
    [SerializeField] private string action;

    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (action == "skills")
        {
            targetObject.SetActive(true);
            bgObject.SetActive(true);
        }
        else if (action == "exit")
        {
            targetObject.SetActive(false);
            bgObject.SetActive(false);
        }
    }
}
