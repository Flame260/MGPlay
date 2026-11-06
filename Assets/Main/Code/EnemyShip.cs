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

    public GameObject explosionPrefab;

    public enum Pattern
    {
        Aimed,
        Straight,
        Spread,
        Ring,
        Spiral
    }
    public Pattern pattern = Pattern.Aimed;
    public int bulletCount = 5;
    public float bulletSpeed = 5f;
    float spiralAngle;

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
        if (pattern == Pattern.Straight)
        {
            FireBullet(Vector3.back);
        }
        else if (pattern == Pattern.Aimed)
        {
            FireBullet(DirectionToPlayer());
        }
        else if (pattern == Pattern.Spread)
        {
            Vector3 center = DirectionToPlayer();
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = (i - (bulletCount - 1) / 2f) * 15f;
                FireBullet(Quaternion.Euler(0, angle, 0) * center);
            }
        }
        else if (pattern == Pattern.Ring)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * (360f / bulletCount);
                FireBullet(Quaternion.Euler(0, angle, 0) * Vector3.forward);
            }
        }
        else if (pattern == Pattern.Spiral)
        {
            FireBullet(Quaternion.Euler(0, spiralAngle, 0) * Vector3.forward);
            spiralAngle += 25f;
        }

    }

    Vector3 DirectionToPlayer()
    {
        if (player == null)
        {
            return Vector3.back;
        }
        Vector3 dir = player.position - transform.position;
        dir.y=0;
        return dir.normalized;
    }
    void FireBullet(Vector3 dir)
    {
        Vector3 spawnPos = transform.position;
        spawnPos.y = 0;
        GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        b.GetComponent<Bullet>().Setup(dir, bulletSpeed * WaveManager.difficulty, false);
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
        AudioManager.Instance.Play(AudioManager.Instance.enemyDieSound, 0.6f);
        if (drops.Length > 0 && Random.value < dropChance)
        {
            int pick = Random.Range(0, drops.Length);
            Instantiate(drops[pick], transform.position, Quaternion.identity);
        }
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth hit = other.GetComponentInParent<PlayerHealth>();
        if (hit != null)
        {
            
            hit.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}

