using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float shootingInterval;
    public Vector3 bulletOffset;

private float lastBulletTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var worldPoint =
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        transform.position = worldPoint;

    }
}
