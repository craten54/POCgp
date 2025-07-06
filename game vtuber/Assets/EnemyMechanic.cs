using UnityEngine;

public class EnemyMechanic : MonoBehaviour
{
    public static int currentEnemy = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void attackEnemy()
    {
        var player = playerStatus.Instance;
        var targetEnemy = player.enemySlots[currentEnemy];

        // Pastikan target valid sebelum melakukan apa pun
        if (targetEnemy == null || targetEnemy.currentHP <= 0) return;

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
        // Anda bisa menambahkan logika 'else if' di sini untuk skill lainnya
    }
}