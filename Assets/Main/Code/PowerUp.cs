using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isSpread = true;
    public float duration = 8f;
    public float fallSpeed = 2.5f;

    void Update()
    {
        transform.position += Vector3.back * fallSpeed * Time.deltaTime;

        if (transform.position.z < -12)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        AutoShoot shoot = other.GetComponent<AutoShoot>();
        if (shoot != null)
        {
            if (isSpread)
            {
                shoot.StartSpread(duration);
            }
            else
            {
                shoot.StartRapid(duration);
            }
            Destroy(gameObject);
        }
    }
}