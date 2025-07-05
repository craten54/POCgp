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
    }

    public void ResetSelection()
    {
        SelectedCharacter = null;
    }
}
