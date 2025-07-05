using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterSelectionUI : MonoBehaviour
{
    [Header("Preview UI")]
    [SerializeField] private Image previewImage;
    [SerializeField] private TMP_Text previewName;
    [SerializeField] private TMP_Text statText;
    [SerializeField] private TMP_Text skill1Text;
    [SerializeField] private TMP_Text skill2Text;

    public void ShowPreview(CharacterData data)
    {
        previewImage.sprite = data.CharacterSprite;
        previewName.text = $"Nama     : {data.CharacterName}";

        statText.text = $"HP: {data.MaxHealth}\nATK: {data.AttackPower}\nDEF: {data.Defense}";

        if (data.SpecialAbilityName.Length > 0)
            skill1Text.text = data.SpecialAbilityName[0] + ":\n" + data.SpecialAbilityDescription[0];

        if (data.SpecialAbilityName.Length > 1)
            skill2Text.text = data.SpecialAbilityName[1] + ":\n" + data.SpecialAbilityDescription[1];
    }

    public void HidePreview()
    {
        previewImage.sprite = null;
        previewName.text = "Nama     : ";
        statText.text = "Stats";
        skill1Text.text = "Skill 1";
        skill2Text.text = "Skill 2";
    }
}
