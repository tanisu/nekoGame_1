using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAA : MonoBehaviour
{
    public void AMove()
    {
        SceneMove.instance.SceneTransition(0, 1);
    }
}
