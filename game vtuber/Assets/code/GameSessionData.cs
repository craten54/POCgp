using UnityEngine;

public class GameSessionData : MonoBehaviour
{
    // --- Singleton Pattern ---
    public static GameSessionData Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Enum untuk membuat pilihan mode lebih jelas dan aman dari kesalahan ketik
    public enum GameMode { None, Stage, Endurance }

    [Header("Pilihan Sesi")]
    public GameMode SelectedGameMode = GameMode.None;

    // Fungsi untuk mengatur mode yang dipilih
    public void SetGameMode(GameMode mode)
    {
        SelectedGameMode = mode;
        Debug.Log("Mode game yang dipilih: " + SelectedGameMode.ToString());
    }
}