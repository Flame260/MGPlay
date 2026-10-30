using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    public TMP_Text hpText;

    float hitTimer;

    void Start()
    {
        hpText.text = "HP: " + health;
    }

    void Update()
    {
        hitTimer -= Time.deltaTime;

        Renderer r = GetComponent<Renderer>();
        if (r != null && !r.enabled)
        {
            Debug.Log("Player renderer is OFF");
        }
    }

    public void TakeDamage(int amount)
    {
        if (hitTimer > 0) return;

        health -= amount;
        hitTimer = 1.5f;
        hpText.text = "HP: " + health;

        if (health <= 0)
        {
            GameManager2.Instance.GameOver();
        }
    }
    void OnDestroy()
    {
        Debug.Log("PLAYER WAS DESTROYED");
    }

    void OnDisable()
    {
        Debug.Log("PLAYER WAS DISABLED");
    }
}
