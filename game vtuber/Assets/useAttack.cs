using UnityEngine;
using TMPro;

public class useAttack : MonoBehaviour
{
    [SerializeField] private GameObject actionBar;
    [SerializeField] private GameObject skillBar;
    [SerializeField] private GameObject bgObject;
    [SerializeField] private TextMeshProUGUI cancelText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnMouseDown()
    {
        Debug.Log("Teken");
        actionBar.SetActive(false);
        skillBar.SetActive(false);
        bgObject.SetActive(false);
        cancelText.gameObject.SetActive(true);


        playerStatus.goingAttack = true;
    }
}
