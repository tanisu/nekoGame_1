using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
   // private  TargetController[] targets;

    //private void Start()
    //{
    //    targets = GetComponentsInChildren<TargetController>();
    //}

    public int GetTargetsLength()
    {
        return GetComponentsInChildren<TargetController>().Length;
    }
}
