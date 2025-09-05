using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 2f;              

    private float backgroundHeight;  
    private Camera mainCam;
    private Transform topBG;
    private Transform bottomBG;

    void Start()
    {
        // Get the main camera
        mainCam = Camera.main;

        // Find background pieces by name
        bottomBG = GameObject.Find("BG-3D Bottom").transform;
        topBG = GameObject.Find("BG-3D Top").transform;

        // Detect height from one of the backgrounds
        backgroundHeight = topBG.GetComponent<Renderer>().bounds.size.y;

        // Ensure top is actually above bottom at start
        if (bottomBG.position.y > topBG.position.y)
        {
            var temp = topBG;
            topBG = bottomBG;
            bottomBG = temp;
        }
    }

    void Update()
    {
        // Move both backgrounds downward
        topBG.Translate(Vector3.down * speed * Time.deltaTime);
        bottomBG.Translate(Vector3.down * speed * Time.deltaTime);

        // Bottom edge of the camera
        float camBottom = mainCam.transform.position.y - mainCam.orthographicSize;

        // If bottom background is completely out of view, recycle it
        if (bottomBG.position.y + backgroundHeight / 2f < camBottom)
        {
            bottomBG.position = new Vector3(
                bottomBG.position.x,
                topBG.position.y + backgroundHeight,
                bottomBG.position.z
            );

            // Swap references
            var temp = topBG;
            topBG = bottomBG;
            bottomBG = temp;
        }
    }
}
