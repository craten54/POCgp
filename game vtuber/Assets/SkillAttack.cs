using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillAttack : MonoBehaviour
{
    public static string currentAction = "0";
    [SerializeField] private GameObject actionBar;
    [SerializeField] private GameObject skillBar;
    [SerializeField] private GameObject bgObject;
    [SerializeField] private Button cancelText;
    
    public void activateSkill()
    {
        actionBar.SetActive(false);
        skillBar.SetActive(false);
        bgObject.SetActive(false);
        cancelText.gameObject.SetActive(true);

        playerStatus.goingAttack = true;
    }

    public void cancelSkill()
    {
        actionBar.SetActive(true);
        skillBar.SetActive(true);
        bgObject.SetActive(true);
        cancelText.gameObject.SetActive(false);

        playerStatus.goingAttack = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
