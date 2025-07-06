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

    public playerStatus playerStatus;
    private SkillPressEffect skillPress;

    public void SetSelectedSkill(SkillPressEffect selected)
    {
        skillPress = selected;
        Debug.Log("✅ Skill selected: " + selected); // opsional debug
    }

    public void activateSkill()
    {

        if (skillPress.skillUsage > playerStatus.energyPoint)
        {
            Debug.Log("❌ Not enough energy points to use this skill!");
            return;
        }
        actionBar.SetActive(false);
        skillBar.SetActive(false);
        bgObject.SetActive(false);
        cancelText.gameObject.SetActive(true);

        playerStatus.goingAttack = true;

        playerStatus.energyPoint -= skillPress.skillUsage;
        playerStatus.updateEnergyPoint();
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
