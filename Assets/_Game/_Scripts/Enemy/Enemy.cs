using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    Vector3 StartingPos;

    private void Start()
    {
        StartingPos = transform.position;
    }
}
