using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Tambahkan ini jika ingin pindah scene

public class ModeSelectionManager : MonoBehaviour
{
    [Header("Tombol Pilihan Mode")]
    public Button stageModeButton;
    public Button enduranceModeButton;
    public Button confirmButton;

    [Header("Umpan Balik Visual (Highlight)")]
    [Tooltip("GameObject yang akan aktif saat mode dipilih, misal: sebuah border atau glow.")]
    public GameObject stageModeHighlight;
    public GameObject enduranceModeHighlight;

    // Menyimpan pilihan sementara di panel ini
    private GameSessionData.GameMode currentSelection = GameSessionData.GameMode.None;

    void Start()
    {
        // Tambahkan listener ke tombol secara otomatis
        stageModeButton.onClick.AddListener(SelectStageMode);
        enduranceModeButton.onClick.AddListener(SelectEnduranceMode);
        confirmButton.onClick.AddListener(ConfirmSelection);

        // Inisialisasi tampilan UI
        UpdateVisuals();
    }

    public void SelectStageMode()
    {
        currentSelection = GameSessionData.GameMode.Stage;
        Debug.Log("Memilih Stage Mode");
        UpdateVisuals();
    }

    public void SelectEnduranceMode()
    {
        currentSelection = GameSessionData.GameMode.Endurance;
        Debug.Log("Memilih Endurance Mode");
        UpdateVisuals();
    }

    // Fungsi untuk mengupdate tampilan berdasarkan pilihan
    private void UpdateVisuals()
    {
        // Atur highlight berdasarkan pilihan
        stageModeHighlight.SetActive(currentSelection == GameSessionData.GameMode.Stage);
        enduranceModeHighlight.SetActive(currentSelection == GameSessionData.GameMode.Endurance);

        // Aktifkan tombol "Select" hanya jika salah satu mode sudah dipilih
        confirmButton.interactable = (currentSelection != GameSessionData.GameMode.None);
    }

    // Fungsi yang dipanggil oleh tombol "Select"
    public void ConfirmSelection()
    {
        if (currentSelection == GameSessionData.GameMode.None)
        {
            Debug.LogWarning("Tidak ada mode yang dipilih!");
            return;
        }

        // 1. Simpan pilihan ke GameSessionData agar bisa diakses di scene lain
        GameSessionData.Instance.SetGameMode(currentSelection);

        // 2. Lanjutkan ke scene permainan
        Debug.Log("Memulai permainan dengan mode: " + currentSelection.ToString());
        // Ganti "GameScene" dengan nama scene permainan Anda yang sebenarnya
        SceneManager.LoadScene("GameScene");
    }
}
