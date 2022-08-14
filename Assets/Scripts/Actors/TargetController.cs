using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    CircleCollider2D cc2d;

    private void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
    }

    public void OffCollider()
    {
        cc2d.enabled = false;
    }
}
