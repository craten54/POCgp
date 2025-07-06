using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// --- SCRIPT UNTUK MENGELOLA PAUSE MENU DAN POPUP DI DALAM GAME ---
public class PauseMenuManager : MonoBehaviour
{
    [Header("Referensi Panel UI")]
    [Tooltip("Panel utama yang muncul saat game di-pause.")]
    public GameObject pauseMenuPanel;

    [Tooltip("Panel yang muncul saat tombol 'Option' ditekan.")]
    public GameObject optionsMenuPanel;

    [Tooltip("Panel konfirmasi yang muncul saat tombol 'Retreat' ditekan.")]
    public GameObject retreatConfirmationPanel; // --- TAMBAHAN BARU ---

    // Variabel untuk melacak status pause
    public static bool isGamePaused = false;

    void Start()
    {
        // Pastikan semua panel tidak aktif saat game dimulai
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);

        // --- TAMBAHAN BARU ---
        if (retreatConfirmationPanel != null)
        {
            retreatConfirmationPanel.SetActive(false);
        }
    }

    void Update()
    {
        // Cek jika tombol 'Escape' ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // --- FUNGSI-FUNGSI YANG AKAN DIPANGGIL OLEH TOMBOL ---

    // Fungsi untuk melanjutkan permainan (dipanggil oleh tombol "Resume" atau "No")
    public void ResumeGame()
    {

        if (retreatConfirmationPanel != null) retreatConfirmationPanel.SetActive(false); // Pastikan popup retreat juga tertutup

        Time.timeScale = 1f; // Mengembalikan kecepatan waktu game ke normal
        isGamePaused = false;
        Debug.Log("Game Resumed.");
    }

    // Fungsi untuk menjeda permainan
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu di dalam game
        isGamePaused = true;
        Debug.Log("Game Paused.");
    }

    // --- FUNGSI BARU UNTUK RETREAT ---
    // Fungsi ini akan dipanggil oleh tombol "Retreat" di UI Anda
    public void ShowRetreatConfirmation()
    {
        Time.timeScale = 0f; // Hentikan permainan
        isGamePaused = true;
        if (retreatConfirmationPanel != null)
        {
            retreatConfirmationPanel.SetActive(true);
        }
        Debug.Log("Retreat confirmation popup shown.");
    }
    // Tombol "Yes" pada popup akan memanggil fungsi LoadMainMenu() di bawah ini.
    // Tombol "No" pada popup akan memanggil fungsi ResumeGame() di atas.
    // --- AKHIR FUNGSI BARU ---

    public void ShowOptionsMenu()
    {
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    public void HideOptionsMenu()
    {
        optionsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    // Fungsi untuk kembali ke Menu Utama (dipanggil oleh tombol "Menu" atau "Yes")
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        isGamePaused = false;

        // Ganti "MainMenu" dengan nama scene menu utama Anda yang sebenarnya
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading Main Menu...");
    }
}
