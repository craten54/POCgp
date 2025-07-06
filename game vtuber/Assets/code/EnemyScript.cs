using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyScript : MonoBehaviour
{
    private GameObject hoverLight;
    [SerializeField] private int enemyID;
    private bool isHovering = false;
    public bool clickOnce = false;
    [SerializeField] private GameObject actionBar;
    [SerializeField] private Button cancelText;

    void Start()
    {
        Transform child = transform.Find("lightHover");
        if (child != null)
        {
            hoverLight = child.gameObject;
            hoverLight.SetActive(false);
        }
        else
        {
            Debug.LogWarning("lightHover not found!");
        }
    }

    void Update()
    {
        // Ray dari mouse ke dunia 2D
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.transform == transform)
        {
            // Mouse berada di atas objek ini
            if (!isHovering && playerStatus.goingAttack)
            {
                isHovering = true;
                hoverLight?.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!clickOnce && SkillAttack.skillAOE)
                {
                    SkillAttack.skillAOE = false;
                    EnemyMechanic.attackEnemy();
                    actionBar.SetActive(true);
                    cancelText.gameObject.SetActive(false);
                    playerStatus.goingAttack = false;
                    SkillAttack.removelightAOE();
                    clickOnce = false;
                    isHovering = false;
                    hoverLight?.SetActive(false);
                }
                else if (!clickOnce)
                {
                    clickOnce = true;
                    EnemyMechanic.currentEnemy = enemyID;
                    Debug.Log("Target dipilih: " + enemyID);
                    hoverLight.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 34);
                }
                else if (EnemyMechanic.currentEnemy == enemyID)
                {
                    EnemyMechanic.attackEnemy();
                    actionBar.SetActive(true);
                    cancelText.gameObject.SetActive(false);
                    playerStatus.goingAttack = false;
                    hoverLight.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 34);
                    clickOnce = false;
                    isHovering = false;
                    hoverLight?.SetActive(false);
                }
            }
        }
        else
        {
            // Mouse tidak lagi di atas objek
            if (isHovering && !clickOnce && !SkillAttack.skillAOE)
            {
                isHovering = false;
                hoverLight?.SetActive(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (clickOnce)
                {
                    clickOnce = false;
                    hoverLight.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 34);
                }
            }
        }
    }
}
