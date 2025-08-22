using UnityEngine;

public class Blinking : MonoBehaviour
{private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }
}
