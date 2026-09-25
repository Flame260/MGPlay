using UnityEngine;

public class coin : MonoBehaviour
{
    
    public float floatHeight = 0.3f;   
    public float floatSpeed = 2f;      

    
    public bool rotate = true;
    public float rotateSpeed = 100f;  
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        
        if (rotate)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        }
    }
}

