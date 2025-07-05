using UnityEngine;
using UnityEngine.UI;

public class CharacterResetBackButton : MonoBehaviour
{
    [SerializeField] private CharacterSelectionUI characterUI; // drag the UI Manager

    public void OnSelect()
    {
        CharacterSelectionManager.Instance.ResetSelection();
        characterUI.HidePreview();
    }
}
