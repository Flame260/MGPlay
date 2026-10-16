using UnityEngine;
using UnityEngine.EventSystems;
public class DragMove : MonoBehaviour
{
    public float padding = 0.6f;

    Camera cam;
    Plane plane;
    Vector3 grabOffset;
    bool dragging;

    void Start()
    {
        cam = Camera.main;
        plane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0f));
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            dragging = true;
            grabOffset = transform.position - PointerWorld();
        }

        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if(dragging && Input.GetMouseButton(0))
        {
            Vector3 p = PointerWorld() + grabOffset;
            transform.position = ClampToScreen(p);
        }
    }

    Vector3 PointerWorld()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        plane.Raycast(ray, out float distance);
        return ray.GetPoint(distance);


    }
    Vector3 ClampToScreen(Vector3 p)
    {
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;
        Vector3 c = cam.transform.position;

        p.x = Mathf.Clamp(p.x, c.x - halfW + padding, c.x + halfW - padding);
        p.z = Mathf.Clamp(p.z, c.z - halfH + padding, c.z + halfH - padding);
        p.y = transform.position.y;
        return p;
    }
}
