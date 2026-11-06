using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public TMP_Text hpText;

    float hitTimer;

    void Start()
    {
        hpText.text = "HP: " + health;
    }

    void Update()
    {
        hitTimer -= Time.deltaTime;

    }

    public void TakeDamage(int amount)
    {
        if (hitTimer > 0) return;
        AudioManager.Instance.Play(AudioManager.Instance.hurtSound, 1f);

        health -= amount;
        hitTimer = 1.5f;
        hpText.text = "HP: " + health;

        if (health <= 0)
        {
            GameManager2.Instance.GameOver();
        }
    }
   
}
