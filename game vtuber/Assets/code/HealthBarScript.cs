using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider; // Slider untuk health bar

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health; // Set nilai maksimum health
        slider.value = health; // Set nilai awal health
    }

    public void SetHealth(int health)
    {
        slider.value = health; // Update nilai health bar
    }
}
