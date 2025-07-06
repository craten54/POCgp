using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

// --- CLASS BARU UNTUK MENYIMPAN STAT DENGAN RAPI ---
[System.Serializable]
public class CharacterStats
{
    public string characterName;
    public int maxHP;
    public int currentHP;
    public int attack;
    [Range(0, 100)]
    public float damageReductionPercent;

    public CharacterStats(string name, int hp, int atk, float dmgRed)
    {
        characterName = name;
        maxHP = hp;
        currentHP = hp;
        attack = atk;
        damageReductionPercent = dmgRed;
    }
}


public class playerStatus : MonoBehaviour
{
    // --- SINGLETON PATTERN ---
    public static playerStatus Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    // --- DATA STATISTIK TERPUSAT ---
    [Header("Statistik Player")]
    public CharacterStats playerStats;
    public int energyPoint = 0;
    public int maxenergyPoint = 6;

    [Header("Slot Musuh (Maksimal 5)")]
    public CharacterStats[] enemySlots = new CharacterStats[5];
    
    [Header("Referensi Visual Musuh")]
    public GameObject[] enemyVisuals = new GameObject[5];

    [Header("Buffs")]
    public int pendengarSetiaCount = 0; // Variabel baru untuk buff

    // Variabel kontrol alur
    public static bool goingAttack = false;
    public int round = 0;
    public int turnIndex = 1;
    private int enduranceLoopCount = 0; // Untuk melacak loop di mode Endurance

    [Header("Referensi UI")]
    public TextMeshProUGUI hpText;
    public List<GameObject> pointImages;
    public TextMeshProUGUI roundText; // Variabel baru untuk teks ronde
    public TextMeshProUGUI buffText; // Referensi UI baru untuk buff

    void Start()
    {
        playerStats = new CharacterStats("VTuber", 100, 50, 10f);

        if (GameSessionData.Instance == null || GameSessionData.Instance.SelectedGameMode == GameSessionData.GameMode.None)

        {

            Debug.LogWarning("GameSessionData tidak ditemukan atau mode belum dipilih. Default ke Stage Mode.");

            // Jika Anda ingin ini berfungsi, pastikan GameObject dengan GameSessionData ada di scene

            // atau buat instance sementara untuk testing.


        }

        pendengarSetiaCount = 2;
        playerStats.maxHP += 10 * pendengarSetiaCount; // +20 max HP
        playerStats.currentHP = playerStats.maxHP;   // Heal penuh ke HP baru
        playerStats.attack += 3 * pendengarSetiaCount;   // +6 Attack
        Debug.Log("Mode Tes: Memulai dengan " + pendengarSetiaCount + " buff Pendengar Setia.");

        // 2. Atur ronde awal secara manual
        // Kita set ke 5 karena executeRound() akan menambahkannya menjadi 6.
        round = 7;
        Debug.Log("Mode Tes: Melompat ke Ronde 6.");

        UpdateBuffUI();

        // --- LOGIKA BARU UNTUK TESTING ---
        // Cek apakah Anda mengatur ronde awal di Inspector untuk testing.
        if (round > 0)
        {
            // Jika ya, langsung spawn untuk ronde tersebut tanpa increment.
            Debug.Log("--- MEMULAI TES UNTUK RONDE " + round + " ---");
            if (roundText != null)
            {
                roundText.text = "Round: " + round;
            }
            SpawnEnemiesForRound(round);
            UpdateEnemyVisuals();
        }
        else
        {
            // Jika tidak (nilai default 0), mulai game dari awal secara normal.
            executeRound();
        }
    }

    void Update()
    {
        updateHP();
        updateEnergyPoint();
        UpdateEnemyVisuals();
        if (Input.GetKeyDown(KeyCode.B))
        {
            AddPendengarSetiaBuff();
        }
    }

    public void AddPendengarSetiaBuff()
    {
        pendengarSetiaCount++;
        Debug.Log("Mendapat buff Pendengar Setia! Total: " + pendengarSetiaCount);

        // Tambahkan stat ke pemain
        playerStats.maxHP += 10;
        playerStats.currentHP += 10; // Heal sebesar HP yang didapat
        playerStats.attack += 3;

        // Pastikan HP tidak melebihi Max HP baru
        if (playerStats.currentHP > playerStats.maxHP)
        {
            playerStats.currentHP = playerStats.maxHP;
        }

        // Update UI untuk menampilkan perubahan
        UpdateBuffUI();
    }

    // Fungsi baru untuk mengupdate UI buff
    public void UpdateBuffUI()
    {
        if (buffText != null)
        {
            if (pendengarSetiaCount > 0)
            {
                buffText.gameObject.SetActive(true);
                buffText.text = "Pendengar Setia: " + pendengarSetiaCount;
            }
            else
            {
                buffText.gameObject.SetActive(false);
            }
        }
    }

    // --- LOGIKA RONDE DAN SPAWN MUSUH ---
    public void executeRound()
    {
        if (GameSessionData.Instance.SelectedGameMode == GameSessionData.GameMode.Stage && round >= 8)
        {
            Debug.Log("Selamat! Anda telah menyelesaikan Mode Stage!");
            return;
        }

        round++;
        turnIndex = 1;
        energyPoint += 2;
        if (energyPoint > maxenergyPoint) energyPoint = maxenergyPoint;

        Debug.Log("--- Memulai Ronde " + round + " ---");

        // --- BLOK KODE YANG DIPERBARUI ---
        // Update teks ronde di UI, dengan pengecekan untuk menghindari error
        if (roundText != null)
        {
            roundText.text = "Round: " + round;
        }
        else
        {
            Debug.LogWarning("Referensi 'roundText' belum di-assign di Inspector.");
        }
        // --- AKHIR BLOK KODE YANG DIPERBARUI ---

        SpawnEnemiesForRound(round);
        UpdateEnemyVisuals();
    }

    // Fungsi utama untuk memunculkan musuh berdasarkan ronde
    private void SpawnEnemiesForRound(int roundNumber)
    {
        // Bersihkan slot musuh dari ronde sebelumnya
        for (int i = 0; i < enemySlots.Length; i++) enemySlots[i] = null;

        int spawnPatternRound = roundNumber;

        // Logika untuk Mode Endurance
        if (GameSessionData.Instance.SelectedGameMode == GameSessionData.GameMode.Endurance && roundNumber > 8)
        {
            // (roundNumber - 9) % 4 akan menghasilkan 0, 1, 2, 3
            // Kita tambahkan 5 untuk mendapatkan pola ronde 5, 6, 7, 8
            spawnPatternRound = ((roundNumber - 9) % 4) + 5;
            
            // Hitung berapa kali loop sudah terjadi untuk meningkatkan kekuatan musuh
            enduranceLoopCount = (roundNumber - 5) / 4;
            Debug.Log("Endurance Loop ke-" + enduranceLoopCount + ". Menggunakan pola spawn dari Ronde " + spawnPatternRound);
        }

        // Skala kekuatan untuk mode Endurance
        float statMultiplier = 1.0f + (0.2f * enduranceLoopCount); // +20% stat per loop

        // Logika spawn berdasarkan pola ronde
        switch (spawnPatternRound)
        {
            case 1:
                AddEnemyWithScaling("Lurker", 100, 10, 0f, statMultiplier, 0);
                AddEnemyWithScaling("Lurker", 100, 10, 0f, statMultiplier, 1);
                break;
            case 2:
            case 3:
            case 4:
                SpawnRandomAnomaly(statMultiplier);
                SpawnRandomAnomaly(statMultiplier);
                SpawnRandomUnit(statMultiplier);
                break;
            case 5:
                AddEnemyWithScaling("Lurker", 100, 10, 0f, statMultiplier, 0);
                AddEnemyWithScaling("Lurker", 100, 10, 0f, statMultiplier, 1);
                break;
            case 6:
                SpawnRandomAnomaly(statMultiplier);
                SpawnRandomAnomaly(statMultiplier);
                SpawnRandomAnomaly(statMultiplier);
                SpawnRandomUnit(statMultiplier);
                SpawnRandomUnit(statMultiplier);
                break;
            case 7:
                for (int i = 0; i < 4; i++) SpawnRandomAnomaly(statMultiplier);
                SpawnRandomUnit(statMultiplier);
                break;
            case 8:
                for (int i = 0; i < 5; i++) SpawnRandomAnomaly(statMultiplier);
                break;
            default:
                Debug.LogWarning("Tidak ada logika spawn untuk ronde " + roundNumber);
                break;
        }
    }

    // Tambahkan fungsi ini di dalam script playerStatus.cs

    public void CheckForNextRound()
    {
        // Cek setiap slot musuh
        foreach (CharacterStats enemy in enemySlots)
        {
            // Jika kita menemukan satu musuh saja yang masih ada dan HP-nya di atas 0...
            if (enemy != null && enemy.currentHP > 0)
            {
                // ...maka ronde belum selesai. Keluar dari fungsi.
                return;
            }
        }

        // Jika loop selesai tanpa menemukan musuh yang hidup,
        // artinya semua musuh sudah kalah. Saatnya memulai ronde berikutnya!
        Debug.Log("Semua musuh di ronde " + round + " telah dikalahkan!");
        executeRound();
    }

    // Menambahkan musuh ke slot kosong pertama dengan stat yang sudah diskalakan
    private void AddEnemyWithScaling(string name, int baseHp, int baseAtk, float dmgRed, float multiplier, int specificSlot = -1)
    {
        int finalHp = Mathf.RoundToInt(baseHp * multiplier);
        int finalAtk = Mathf.RoundToInt(baseAtk * multiplier);
        
        if (specificSlot != -1)
        {
            enemySlots[specificSlot] = new CharacterStats(name, finalHp, finalAtk, dmgRed);
            return;
        }

        for (int i = 0; i < enemySlots.Length; i++)
        {
            if (enemySlots[i] == null)
            {
                enemySlots[i] = new CharacterStats(name, finalHp, finalAtk, dmgRed);
                return;
            }
        }
    }

    // Memunculkan Anomali acak (bukan Lurker)
    private void SpawnRandomAnomaly(float multiplier)
    {
        int rand = Random.Range(0, 4); // Spammer, Heckler, Backseat, Promotor
        switch (rand)
        {
            case 0: AddEnemyWithScaling("Spammer", 120, 15, 5f, multiplier); break;
            case 1: AddEnemyWithScaling("Heckler", 150, 20, 10f, multiplier); break;
            case 2: AddEnemyWithScaling("Backseat Gamer", 80, 5, 0f, multiplier); break;
            case 3: AddEnemyWithScaling("Promotor Judol", 180, 0, 20f, multiplier); break;
        }
    }

    // Memunculkan unit acak (bisa Lurker atau Anomali)
    private void SpawnRandomUnit(float multiplier)
    {
        if (Random.value < 0.3f) // 30% kemungkinan Lurker
        {
            AddEnemyWithScaling("Lurker", 100, 10, 0f, multiplier);
        }
        else
        {
            SpawnRandomAnomaly(multiplier);
        }
    }

    // --- FUNGSI MANAJEMEN VISUAL & UI ---
    public void UpdateEnemyVisuals()
    {
        for (int i = 0; i < enemySlots.Length; i++)
        {
            if (enemyVisuals[i] != null)
            {
                bool shouldBeActive = (enemySlots[i] != null && enemySlots[i].currentHP > 0);
                enemyVisuals[i].SetActive(shouldBeActive);
            }
        }
    }

    public void updateHP()
    {
        hpText.text = "HP : " + playerStats.currentHP + " / " + playerStats.maxHP;
    }

    public void updateEnergyPoint()
    {
        for (int i = 0; i < pointImages.Count; i++)
        {
            Image img = pointImages[i].GetComponent<Image>();
            img.color = (i < energyPoint)
                ? new Color32(255, 255, 255, 255)
                : new Color32(63, 55, 55, 255);
        }
    }
}
