using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class playerStatus : MonoBehaviour
{
    public float playerHP = 100f;
    public float maxplayerHP = 100f;
    public int energyPoint = 0;
    public int maxenergyPoint = 6;
    public int attackPower = 50;
    public int playerDEF = 10;
    public float[] enemyHP = { 100f, 100f, 100f, 100f, 100f };
    public float[] maxenemyHP = { 100f, 100f, 100f, 100f, 100f };
    public int[] enemyATK = { 10, 10, 10, 10, 10 };
    public int[] enemyDEF = { 10, 10, 10, 10, 10 };
    public static bool goingAttack = false;

    public TextMeshProUGUI hpText;
    public List<GameObject> pointImages;

    public int round = 0;
    public int turnIndex = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        executeRound();
        updateHP();
        updateEnergyPoint();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void executeRound()
    {
        round++;
        turnIndex = 1;
        energyPoint += 2;
    }

    public void updateHP()
    {
        hpText.text = "HP : " + playerHP + " / " + maxplayerHP;
    }

    public void updateEnergyPoint()
    {
        for (int i = 0; i < pointImages.Count; i++)
        {
            GameObject go = pointImages[i];
            Image img = go.GetComponent<Image>();
            img.color = (i < energyPoint)
                ? new Color32(255, 255, 255, 255)
                : new Color32(63, 55, 55, 255);
        }
    }
}
