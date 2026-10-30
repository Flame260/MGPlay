using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public bool fromPlayer = true;
    Vector3 direction = Vector3.forward;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void Setup(Vector3 newDirection, float newSpeed, bool isPLayerBullet)
    {
        direction = newDirection.normalized;
        speed = newSpeed;
        fromPlayer = isPLayerBullet;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " touched " + other.name);
        if (fromPlayer)
        {
            EnemyShip enemy = other.GetComponent<EnemyShip>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
                Destroy(gameObject);
            }
        }
        else
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
}