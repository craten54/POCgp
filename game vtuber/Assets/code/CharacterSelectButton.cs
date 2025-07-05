using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectButton : MonoBehaviour
{
    [SerializeField] private CharacterData characterData; // drag .asset here
    [SerializeField] private CharacterSelectionUI characterUI; // drag the UI Manager

    public void OnSelect()
    {
        Debug.Log("Button clicked: " + characterData.CharacterName); //debug log for button click (testing purposes)

        CharacterSelectionManager.Instance.SetCharacter(characterData);
        characterUI.ShowPreview(characterData);
    }
}
