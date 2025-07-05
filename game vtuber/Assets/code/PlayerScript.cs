using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    int maxHealth = 100; // Nilai maksimum health
    int currentHealth; // Nilai health saat ini
    int attackPower = 20; // Nilai serangan
    int defense = 10; // Nilai defense

    public HealthBarScript healthBar; // Referensi ke HealthBarScript untuk mengupdate health bar
    public float spriteBlinkingTimer = 0.0f;
    public float spriteBlinkingMiniDuration = 0.1f;
    public float spriteBlinkingTotalTimer = 0.0f;
    public float spriteBlinkingTotalDuration = 1.0f;
    public bool startBlinking = false;

    void Start()
    {
        currentHealth = maxHealth; // Inisialisasi health saat game dimulai
        healthBar.SetMaxHealth(maxHealth); // Set nilai maksimum health pada health bar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Contoh input untuk menyerang
        {
            TakeDamage(20); // Panggil fungsi Attack ketika tombol ditekan
            startBlinking = true; // Mulai efek blinking ketika menerima damage
        }
        if(startBlinking)
        { 
            SpriteBlinkingEffect();
        }
    }

    void TakeDamage(int damage)
    {
        // Hitung damage yang diterima setelah mempertimbangkan defense
        int damageTaken = Mathf.Max(damage - defense, 0);
        currentHealth -= damageTaken; // Kurangi health dengan damage yang diterima

        healthBar.SetHealth(currentHealth); // Update health bar dengan nilai health saat ini

        if (currentHealth <= 0)
        {
            Die(); // Panggil fungsi Die jika health habis
        }
    }

     private void SpriteBlinkingEffect()
     {
       spriteBlinkingTotalTimer += Time.deltaTime;
       if(spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
       {
	        startBlinking = false;
	        spriteBlinkingTotalTimer = 0.0f;
	        this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   // according to 
                     //your sprite
	        return;
         }
	
	spriteBlinkingTimer += Time.deltaTime;
	if(spriteBlinkingTimer >= spriteBlinkingMiniDuration)
	{
		spriteBlinkingTimer = 0.0f;
		if (this.gameObject.GetComponent<SpriteRenderer> ().enabled == true) {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = false;  //make changes
		} else {
			this.gameObject.GetComponent<SpriteRenderer> ().enabled = true;   //make changes
		}
	}
}
    
    void Die()
    {
        // Logika ketika pemain mati, misalnya menampilkan pesan atau mengakhiri game
        Debug.Log("Player has died.");
        // Tambahkan logika lain sesuai kebutuhan, seperti mengaktifkan layar Game Over
    }



}
