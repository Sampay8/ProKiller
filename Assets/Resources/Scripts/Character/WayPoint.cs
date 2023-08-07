using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    private System.Random _rand = new System.Random();

    public Transform GetNewPointToMove()
    {
        return _points[_rand.Next(0, _points.Count)];
    }
}
