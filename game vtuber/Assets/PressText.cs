using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PressTextEffect : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject bgObject;
    [SerializeField] private Button useButton;
    [SerializeField] private string action;

    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (action == "skills")
        {
            useButton.interactable = false;
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
