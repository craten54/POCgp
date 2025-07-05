using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Character Information")]
    [SerializeField] private int characterID;
    [SerializeField] private string characterName; // Nama karakter
    [SerializeField] private Sprite characterSprite; // Gambar karakter

    [Header("Character Stats")]
    [SerializeField] private int maxHealth; // Nilai health karakter
    [SerializeField] private int attackPower; // Nilai serangan karakter
    [SerializeField] private int defense; // Nilai pertahanan karakter

    [Header("Character Abilities")]
    [SerializeField] private string[] specialAbilityName; // Nama kemampuan khusus
    [SerializeField] private string[] specialAbilityDescription; // Deskripsi kemampuan khusus

    // Add public getters to access the data
    public int CharacterID => characterID;
    public int MaxHealth => maxHealth;
    public int AttackPower => attackPower;
    public int Defense => defense;
    public string CharacterName => characterName;
    public Sprite CharacterSprite => characterSprite;
    public string[] SpecialAbilityName => specialAbilityName;
    public string[] SpecialAbilityDescription => specialAbilityDescription;

}
