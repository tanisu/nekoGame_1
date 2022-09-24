using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour
{
    //public bool isBonus;

    public void BonusStart()
    {
        Debug.Log("Bonus Start");
        StartCoroutine(_bonus());
    }


    IEnumerator _bonus()
    {

        //isBonus = true;
        yield return new WaitForSeconds(3f);
        Debug.Log("Bonus End");
        //isBonus = false;
        SceneMove.instance.StageClear();
    }
}
