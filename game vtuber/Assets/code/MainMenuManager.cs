using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- SCRIPT UNTUK MENGELOLA NAVIGASI MENU DALAM SATU SCENE ---
// Cara Penggunaan di Unity Editor:
// 1. Buat GameObject kosong dan beri nama "MenuManager".
// 2. Pasang (attach) script ini ke GameObject "MenuManager".
// 3. Buat beberapa Panel UI untuk setiap menu (MainMenu, CharacterSelection, etc.).
// 4. Buat SATU Panel UI tambahan untuk "Coming Soon" dan nonaktifkan secara default.
// 5. Masukkan semua GameObject Panel tersebut ke dalam field yang sesuai pada Inspector.

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [Tooltip("Masukkan semua panel menu utama di sini.")]
    public GameObject mainMenuPanel;
    public GameObject characterSelectionPanel;
    public GameObject stageSelectionPanel;
    public GameObject shopPanel;
    public GameObject collectionPanel;
    public GameObject settingsPanel;

    [Header("Overlay Panels")]
    [Tooltip("Panel ini akan muncul di atas panel lain.")]
    public GameObject comingSoonPanel; // Variabel yang hilang ditambahkan kembali

    void Start()
    {
        // Saat game dimulai, pastikan hanya menu utama yang aktif.
        ShowMainMenu();
        // Pastikan panel overlay tidak aktif saat mulai.
        if (comingSoonPanel != null) comingSoonPanel.SetActive(false);
    }

    // --- FUNGSI UNTUK MENONAKTIFKAN SEMUA PANEL UTAMA---
    private void HideAllPanels()
    {
        // Nonaktifkan semua panel yang terhubung untuk memastikan tidak ada yang tumpang tindih.
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (characterSelectionPanel != null) characterSelectionPanel.SetActive(false);
        if (stageSelectionPanel != null) stageSelectionPanel.SetActive(false);
        if (shopPanel != null) shopPanel.SetActive(false);
        if (collectionPanel != null) collectionPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    // --- FUNGSI-FUNGSI NAVIGASI PANEL UTAMA ---

    public void ShowMainMenu()
    {
        HideAllPanels();
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }

    // Fungsi ini akan dipanggil oleh tombol "Mulai Stream"
    public void ShowCharacterSelection()
    {
        HideAllPanels();
        if (characterSelectionPanel != null) characterSelectionPanel.SetActive(true);
        if (comingSoonPanel != null)
        {
            comingSoonPanel.SetActive(false);
        }
    }

    // Fungsi ini akan dipanggil setelah memilih karakter
    public void ShowStageSelection()
    {
        HideAllPanels();
        if (stageSelectionPanel != null) stageSelectionPanel.SetActive(true);
    }

    // Fungsi ini akan dipanggil oleh tombol "Toko VTuber"
    public void ShowShop()
    {
        HideAllPanels();
        if (shopPanel != null) shopPanel.SetActive(true);
    }

    // Fungsi ini akan dipanggil oleh tombol "Koleksi VTuber"
    public void ShowCollection()
    {
        HideAllPanels();
        if (collectionPanel != null) collectionPanel.SetActive(true);
    }

    // Fungsi ini akan dipanggil oleh tombol "Pengaturan"
    public void ShowSettings()
    {
        HideAllPanels();
        if (settingsPanel != null) settingsPanel.SetActive(true);
        if (comingSoonPanel != null)
        {
            comingSoonPanel.SetActive(false);
        }
    }

    // --- FUNGSI UNTUK PANEL OVERLAY ---

    // Fungsi ini akan dipanggil oleh tombol yang fiturnya belum siap
    public void ShowComingSoon()
    {
        // Jangan panggil HideAllPanels() agar panel ini muncul di atas panel yang sedang aktif.
        if (comingSoonPanel != null)
        {
            comingSoonPanel.SetActive(true);
        }
    }

    // Fungsi ini akan dipanggil oleh tombol "Close" di dalam panel Coming Soon
    public void HideComingSoon()
    {
        if (comingSoonPanel != null)
        {
            comingSoonPanel.SetActive(false);
        }
    }

    // --- FUNGSI-FUNGSI LAIN ---

    // Fungsi ini akan dipanggil saat tombol "Exit" ditekan
    public void KeluarGame()
    {
        Debug.Log("Tombol Exit ditekan!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
