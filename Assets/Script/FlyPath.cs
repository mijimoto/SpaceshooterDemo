using UnityEngine;

public class FlyPath : MonoBehaviour
{
public Waypoint[] waypoints;

private void Reset() => waypoints = GetComponentsInChildren<Waypoint>();
}