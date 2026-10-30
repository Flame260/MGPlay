using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.15f;

    float timer;
    float spreadTimer;
    float rapidTimer;

    void Update()
    {
        timer += Time.deltaTime;
        spreadTimer -= Time.deltaTime;
        rapidTimer -= Time.deltaTime;

        float currentRate = fireRate;
        if (rapidTimer > 0)
        {
            currentRate = fireRate / 2;
        }

        if (timer >= currentRate)
        {
            timer = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        if (spreadTimer > 0)
        {
            SpawnBullet(-12);
            SpawnBullet(0);
            SpawnBullet(12);
        }
        else
        {
            SpawnBullet(0);
        }
    }

    void SpawnBullet(float angle)
    {
        Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        Vector3 spawnPos = transform.position + Vector3.forward;
        spawnPos.y = 0;
        GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        b.GetComponent<Bullet>().Setup(dir, 20f, true);
    }

    public void StartSpread(float time)
    {
        spreadTimer = time;
    }

    public void StartRapid(float time)
    {
        rapidTimer = time;
    }
}
