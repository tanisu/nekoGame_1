using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneButton : MonoBehaviour
{
    public void Retry()
    {
        SceneMove.instance.SceneTransition(1,1);
    }

    public void DefaultMove()
    {
        SceneMove.instance.SceneTransition(0, 1);
    }
}
