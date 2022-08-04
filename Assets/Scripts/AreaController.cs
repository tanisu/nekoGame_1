using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    Transform[] targets;

    private void Start()
    {
        
        targets = GetComponentsInChildren<Transform>();
    }


}
