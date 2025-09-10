using UnityEngine;

public class Waypoint : MonoBehaviour
{
private void OnDrawGizmos()
{
Gizmos.color = Color.green;
Gizmos.DrawSphere(transform.position, 0.1f);
}
}