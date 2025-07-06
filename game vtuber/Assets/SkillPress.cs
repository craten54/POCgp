using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillPressEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private string originalText;
    [SerializeField] private string action;

    [SerializeField] private TextMeshProUGUI nameSkill;
    [SerializeField] private TextMeshProUGUI typeSkill;
    [SerializeField] private TextMeshProUGUI descSkill;
    [SerializeField] private TextMeshProUGUI epSkill;
    [SerializeField] private Button useButton;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        SkillAttack.currentAction = action;
        useButton.interactable = true;
        
        switch (action)
        {
            case "1":
                nameSkill.text = "Basic Attack";
                typeSkill.text = "ATK";
                descSkill.text = "A normal conversation [Single Attack]";
                epSkill.text = "EP 1";
                break;
            // case "2":
            //     nameSkill.text = "Meme Review";
            //     typeSkill.text = "CC";
            //     descSkill.text = "Just a meme review [AOE Debuff Confused]";
            //     epSkill.text = "EP 3";
            //     break;
            // case "3":
            //     nameSkill.text = "Skillful Move";
            //     typeSkill.text = "ATK";
            //     descSkill.text = "Show them those moves [AOE Attack]";
            //     epSkill.text = "EP 3";
            //     break;
            // case "4":
            //     nameSkill.text = "SUPA ARIGATOU";
            //     typeSkill.text = "SP";
            //     descSkill.text = "Thank you for that superchat [Heal]";
            //     epSkill.text = "EP 4";
            //     break;
            case "2":
                nameSkill.text = "BAN HAMMER";
                typeSkill.text = "UTIL";
                descSkill.text = "Hammer Boom Boom [AOE Attack]";
                epSkill.text = "EP 3";
                break;
            // case "6":
            //     nameSkill.text = "Community Welcome";
            //     typeSkill.text = "BUFF";
            //     descSkill.text = "A nice welcoming chat [BUFF]";
            //     epSkill.text = "EP 2";
            //     break;
            // case "7":
            //     nameSkill.text = "Shoutout!";
            //     typeSkill.text = "BUFF";
            //     descSkill.text = "Shoutout to you [BUFF]";
            //     epSkill.text = "EP 3";
            //     break;
            case "3":
                nameSkill.text = "Special Thanks";
                typeSkill.text = "Heal";
                descSkill.text = "A very grateful appreciation [Heal]";
                epSkill.text = "EP 3";
                break;
        }
    }
}
