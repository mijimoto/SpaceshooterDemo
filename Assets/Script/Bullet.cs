using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

public float flySpeed;

// Update is called once per frame
void Update()
{
var newPosition = transform.position;
newPosition.y += Time.deltaTime * flySpeed;
transform.position = newPosition;
}
}
