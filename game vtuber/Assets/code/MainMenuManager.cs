using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- SCRIPT UNTUK MENGELOLA NAVIGASI MENU DALAM SATU SCENE ---
// Cara Penggunaan di Unity Editor:
// 1. Buat GameObject kosong dan beri nama "MenuManager".
// 2. Pasang (attach) script ini ke GameObject "MenuManager".
// 3. Buat beberapa Panel UI (GameObject -> UI -> Panel) untuk setiap menu:
//    - MainMenuPanel
//    - CharacterSelectionPanel
//    - StageSelectionPanel
//    - ShopPanel
//    - etc.
// 4. Masukkan semua GameObject Panel tersebut ke dalam field yang sesuai pada Inspector.
// 5. Untuk setiap tombol, pada komponen Button di bagian OnClick():
//    a. Tekan tanda '+'.
//    b. Drag GameObject "MenuManager" ke dalam field object.
//    c. Dari dropdown fungsi, pilih "MenuNavigationManager" -> fungsi yang sesuai (misal: ShowCharacterSelection).

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [Tooltip("Masukkan semua panel menu yang akan diatur di sini.")]
    public GameObject mainMenuPanel;
    public GameObject characterSelectionPanel;
    public GameObject stageSelectionPanel; // Panel baru untuk pemilihan stage
    public GameObject shopPanel;
    public GameObject collectionPanel;
    public GameObject settingsPanel;

    void Start()
    {
        // Saat game dimulai, pastikan hanya menu utama yang aktif.
        // Semua panel lain akan dinonaktifkan.
        ShowMainMenu();
    }

    // --- FUNGSI UNTUK MENONAKTIFKAN SEMUA PANEL ---
    private void HideAllPanels()
    {
        // Nonaktifkan semua panel yang terhubung untuk memastikan tidak ada yang tumpang tindih.
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (characterSelectionPanel != null) characterSelectionPanel.SetActive(false);
        if (stageSelectionPanel != null) stageSelectionPanel.SetActive(false); // Tambahkan panel stage ke fungsi ini
        if (shopPanel != null) shopPanel.SetActive(false);
        if (collectionPanel != null) collectionPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    // --- FUNGSI-FUNGSI YANG AKAN DIPANGGIL OLEH TOMBOL ---

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
    }

    // Fungsi ini akan dipanggil saat tombol "Exit" ditekan
    public void KeluarGame()
    {
        // Pesan ini akan muncul di console Unity untuk pengetesan
        Debug.Log("Tombol Exit ditekan!");

        // Jika game sedang berjalan di dalam Unity Editor...
#if UNITY_EDITOR
        // ...maka hentikan Play Mode.
        UnityEditor.EditorApplication.isPlaying = false;

        // Jika game berjalan di versi build (bukan di editor)...
#else
        // ...maka tutup aplikasi.
        Application.Quit();
#endif
    }
}
