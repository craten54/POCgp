using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance { get; private set; }

    public CharacterData SelectedCharacter { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCharacter(CharacterData character)
    {
        SelectedCharacter = character;
        Debug.Log("🟢 SetCharacter called: " + character.CharacterName);
        Debug.Log("📦 GameObject name holding the manager: " + gameObject.name);
    }

    public void ResetSelection()
    {
        SelectedCharacter = null;
    }

    private void Start()
    {
        if (SelectedCharacter != null)
        {
            Debug.Log("✅ CharacterSelectionManager Started - SelectedCharacter: " + SelectedCharacter.CharacterName);
        }
        else
        {
            Debug.Log("🟡 CharacterSelectionManager Started - No character selected yet.");
        }
    }
}
