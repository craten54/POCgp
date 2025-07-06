using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyMechanic : MonoBehaviour
{
    public static int currentEnemy = 0;
    [SerializeField] private EnemyMechanic enemyMech;
    [SerializeField] private GameObject emote1;
    [SerializeField] private GameObject emote2;
    [SerializeField] private GameObject emote3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static EnemyMechanic Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static IEnumerator WaitCoroutine(GameObject emote, GameObject character)
    {
        Animator animate = character.GetComponent<Animator>();
        
        character.SetActive(true);
        emote.SetActive(true);

        // Reset animasi agar bisa diputar ulang
        animate.Play("Idle_char1"); // pastikan kamu punya animasi idle atau apapun
        animate.Play("Suprise", -1, 0f); // restart dari awal

        yield return new WaitForSeconds(1.0f);

        character.SetActive(false);
        emote.SetActive(false);
    }

    public static void attackEnemy()
    {
        var player = playerStatus.Instance;
        var targetEnemy = player.enemySlots[currentEnemy];

        // Pastikan target valid sebelum melakukan apa pun
        if (targetEnemy == null || targetEnemy.currentHP <= 0) return;

        GameObject char1 = GameObject.Find("Char1Skill");

        // Logika untuk Basic Attack (ID "1")
        if (SkillAttack.currentAction == "1")
        {
            int energyCost = 1;

            if (player.energyPoint >= energyCost)
            {
                player.energyPoint -= energyCost;
                int damageDealt = player.playerStats.attack;
                targetEnemy.currentHP -= damageDealt;

                Debug.Log("Player menyerang " + targetEnemy.characterName + " sebesar " + damageDealt + " damage!");
                Instance.StartCoroutine(WaitCoroutine(Instance.emote3, char1));

                // Clamp HP to 0
                if (targetEnemy.currentHP < 0)
                {
                    targetEnemy.currentHP = 0;
                }

                // --- LOGIKA BARU SAAT MUSUH KALAH ---
                if (targetEnemy.currentHP == 0)
                {
                    Debug.Log(targetEnemy.characterName + " telah dikalahkan!");

                    // Cek apakah musuh yang kalah adalah "Lurker"
                    if (targetEnemy.characterName == "Lurker")
                    {
                        // Peluang 50/50
                        if (Random.value < 0.5f)
                        {
                            // Opsi 1: Berubah menjadi Anomali
                            player.SpawnAnomalyInSlot(currentEnemy);
                        }
                        else
                        {
                            // Opsi 2: Menjadi Pendengar Setia
                            player.AddPendengarSetiaBuff();
                            // Kosongkan slot musuh
                            player.enemySlots[currentEnemy] = null;
                        }
                    }
                    else // Jika musuh yang kalah bukan Lurker
                    {
                        // Langsung kosongkan slot musuh
                        player.enemySlots[currentEnemy] = null;
                    }

                    // Panggil pengecekan untuk ronde berikutnya
                    player.CheckForNextRound();
                }
                player.EndPlayerTurn();

            }
            else
            {
                Debug.LogWarning("Energi tidak cukup untuk Basic Attack!");
            }
        }
        else if (SkillAttack.currentAction == "2")
        {
            int energyCost = 3;

            if (player.energyPoint >= energyCost)
            {
                player.energyPoint -= energyCost;
                int damagePerHit = player.playerStats.attack;

                Debug.Log($"Player menggunakan Skill 2: menyerang semua musuh sebesar {damagePerHit} damage.");
                Instance.StartCoroutine(WaitCoroutine(Instance.emote2, char1));

                for (int i = 0; i < player.enemySlots.Length; i++)
                {
                    var enemy = player.enemySlots[i];

                    if (enemy != null && enemy.currentHP > 0)
                    {
                        enemy.currentHP -= damagePerHit;

                        Debug.Log($"→ Menyerang {enemy.characterName}, sisa HP: {Mathf.Max(enemy.currentHP, 0)}");

                        // Clamp HP
                        if (enemy.currentHP < 0)
                            enemy.currentHP = 0;

                        if (enemy.currentHP == 0)
                        {
                            Debug.Log($"{enemy.characterName} telah dikalahkan!");

                            if (enemy.characterName == "Lurker")
                            {
                                if (Random.value < 0.5f)
                                {
                                    player.SpawnAnomalyInSlot(i);
                                }
                                else
                                {
                                    player.AddPendengarSetiaBuff();
                                    player.enemySlots[i] = null;
                                }
                            }
                            else
                            {
                                player.enemySlots[i] = null;
                            }
                        }
                    }
                }

                // Setelah semua serangan selesai
                player.CheckForNextRound();
                player.EndPlayerTurn();
            }
            else
            {
                Debug.LogWarning("Energi tidak cukup untuk Skill 2!");
            }
        }
        else if (SkillAttack.currentAction == "3")
        {
            int energyCost = 3;

            if (player.energyPoint >= energyCost)
            {
                player.energyPoint -= energyCost;

                int healAmount = (int)(player.playerStats.maxHP * 0.25f);
                Instance.StartCoroutine(WaitCoroutine(Instance.emote1, char1));

                // Clamp HP agar tidak melebihi max
                if (player.playerStats.currentHP > player.playerStats.maxHP)
                {
                    player.playerStats.currentHP = player.playerStats.maxHP;
                }

                Debug.Log($"Player menggunakan Skill 3: menyembuhkan diri sendiri sebesar {healAmount} HP. HP sekarang: {player.playerStats.currentHP}");

                // Selesai, lanjut ke ronde berikutnya
                player.CheckForNextRound();
                player.EndPlayerTurn();
            }
            else
            {
                Debug.LogWarning("Energi tidak cukup untuk Skill 3!");
            }
        }
        // Anda bisa menambahkan logika 'else if' di sini untuk skill lainnya
        char1.SetActive(true);
    }
}