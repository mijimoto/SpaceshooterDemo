using UnityEngine;

public class FlyPath : MonoBehaviour
{
public Waypoint[] waypoints;

public Vector3 this[int index] => waypoints[index].transform.position;

    private void Reset() => waypoints = GetComponentsInChildren<Waypoint>();
private void OnDrawGizmos()
{
if (waypoints == null) return;

Gizmos.color = Color.green;
for (int i = 0; i < waypoints.Length - 1; i++)
{
Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i +
1].transform.position);
}
}
}