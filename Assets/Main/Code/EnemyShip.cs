using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    public int health = 1;
    public float speed = 3f;
    public float swayAmount = 0f;
    public float swaySpeed = 2f;

    public GameObject bulletPrefab;
    public float fireInterval = 0f;
    public bool aimAtPlayer = true;

    public GameObject[] drops;
    public float dropChance = 0.15f;

    float startX;
    float time;
    float shootTimer;
    Transform player;

    void Start()
    {
        startX = transform.position.x;
        PlayerHealth p = FindFirstObjectByType<PlayerHealth>();
        if (p != null)
        {
            player = p.transform;
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        Vector3 pos = transform.position;
        pos.z -= speed * Time.deltaTime;
        if (swayAmount > 0)
        {
            pos.x = startX + Mathf.Sin(time * swaySpeed) * swayAmount;
        }
        transform.position = pos;

        if (pos.z < -12)
        {
            Destroy(gameObject);
        }

        if (fireInterval > 0 && bulletPrefab != null && pos.z < 7)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= fireInterval)
            {
                shootTimer = 0;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector3 dir = Vector3.back;
        if (aimAtPlayer && player != null)
        {
            dir = player.position - transform.position;
            dir.y = 0;
        }

        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().SetDirection(dir);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (drops.Length > 0 && Random.value < dropChance)
        {
            int pick = Random.Range(0, drops.Length);
            Instantiate(drops[pick], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " touched " + other.name);
        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}

