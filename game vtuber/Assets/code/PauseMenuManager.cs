using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// --- SCRIPT UNTUK MENGELOLA PAUSE MENU DI DALAM GAME ---
// Cara Penggunaan di Unity Editor:
// 1. Buat GameObject kosong di scene permainan Anda, beri nama "PauseMenuManager".
// 2. Pasang (attach) script ini ke GameObject tersebut.
// 3. Buat Panel UI untuk menu pause (misal: "PausePanel") dan satu lagi untuk opsi ("OptionsPanel").
// 4. Masukkan kedua panel tersebut ke field yang sesuai di Inspector.
// 5. Atur setiap tombol di dalam panel untuk memanggil fungsi yang benar dari script ini.
// 6. Nonaktifkan kedua panel tersebut secara default.

public class PauseMenuManager : MonoBehaviour
{
    [Header("Referensi Panel UI")]
    [Tooltip("Panel utama yang muncul saat game di-pause.")]
    public GameObject pauseMenuPanel;

    [Tooltip("Panel yang muncul saat tombol 'Option' ditekan.")]
    public GameObject optionsMenuPanel;

    // Variabel untuk melacak status pause
    public static bool isGamePaused = false;

    void Start()
    {
        // Pastikan semua panel tidak aktif saat game dimulai
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false);
    }

    // Update dipanggil setiap frame
    void Update()
    {
        // Cek jika tombol 'Escape' ditekan
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                // Jika sedang pause, panggil fungsi Resume
                ResumeGame();
            }
            else
            {
                // Jika tidak sedang pause, panggil fungsi Pause
                PauseGame();
            }
        }
    }

    // --- FUNGSI-FUNGSI YANG AKAN DIPANGGIL OLEH TOMBOL ---

    // Fungsi untuk melanjutkan permainan (dipanggil oleh tombol "Resume")
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(false); // Pastikan panel opsi juga tertutup
        Time.timeScale = 1f; // Mengembalikan kecepatan waktu game ke normal
        isGamePaused = false;
        Debug.Log("Game Resumed.");
    }

    // Fungsi untuk menjeda permainan (bisa dipanggil oleh tombol pause di layar)
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Menghentikan waktu di dalam game
        isGamePaused = true;
        Debug.Log("Game Paused.");
    }

    // Fungsi untuk menampilkan panel Opsi (dipanggil oleh tombol "Option")
    public void ShowOptionsMenu()
    {
        // Sembunyikan panel pause utama dan tampilkan panel opsi
        pauseMenuPanel.SetActive(false);
        optionsMenuPanel.SetActive(true);
    }

    // Fungsi untuk menyembunyikan panel Opsi (untuk tombol "Back" di dalam panel Opsi)
    public void HideOptionsMenu()
    {
        // Sembunyikan panel opsi dan tampilkan kembali panel pause utama
        optionsMenuPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);
    }

    // Fungsi untuk kembali ke Menu Utama (dipanggil oleh tombol "Menu")
    public void LoadMainMenu()
    {
        // PENTING: Selalu kembalikan timeScale ke 1 sebelum pindah scene
        Time.timeScale = 1f;
        isGamePaused = false;

        // Ganti "MainMenu" dengan nama scene menu utama Anda yang sebenarnya
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading Main Menu...");
    }
}
