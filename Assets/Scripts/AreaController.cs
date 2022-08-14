using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public TargetController[] targets;

    private void Start()
    {
        
        targets = GetComponentsInChildren<TargetController>();
    }


}
