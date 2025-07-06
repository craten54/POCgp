using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SkillAttack : MonoBehaviour
{
    public static string currentAction = "0";
    [SerializeField] private GameObject actionBar;
    [SerializeField] private GameObject skillBar;
    [SerializeField] private GameObject bgObject;
    [SerializeField] private Button cancelText;

    public static bool skillAOE = false;

    public static SkillAttack Instance;

    [SerializeField] private List<GameObject> lightAOE;

    void Awake()
    {
        Instance = this;
    }

    public static List<GameObject> GetLightAOE()
    {
        return Instance.lightAOE;
    }

    public static void showAOE()
    {
        skillAOE = true;
        for (int i = 0; i <= 4 && i < Instance.lightAOE.Count; i++)
        {
            if (Instance.lightAOE[i] != null)
            {
                Instance.lightAOE[i].SetActive(true);
                Instance.lightAOE[i].GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 34);
            }
        }
    }

    public static void removelightAOE()
    {
        for (int i = 0; i <= 4 && i < Instance.lightAOE.Count; i++)
        {
            if (Instance.lightAOE[i] != null)
            {
                Instance.lightAOE[i].SetActive(false);
                Instance.lightAOE[i].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 34);
            }
        }
    }
    
    public void activateSkill()
    {
        if (currentAction == "2")
        {
            showAOE();
        }

        if (currentAction == "3")
        {
            actionBar.SetActive(true);
            skillBar.SetActive(false);
            bgObject.SetActive(false);
            EnemyMechanic.attackEnemy();
            var player = playerStatus.Instance;
            actionBar.SetActive(true);
            player.updateHP();
        }
        else
        {
            actionBar.SetActive(false);
            skillBar.SetActive(false);
            bgObject.SetActive(false);
            cancelText.gameObject.SetActive(true);

            playerStatus.goingAttack = true;
        }
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
