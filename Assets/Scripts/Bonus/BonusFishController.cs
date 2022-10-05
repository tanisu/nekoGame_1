using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFishController : MonoBehaviour
{
    BonusPoolController pool;
    Rigidbody2D rb2d;
    public BonusData fishData;


    public void Init()
    {
        pool = transform.parent.GetComponent<BonusPoolController>();
        rb2d = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        rb2d.AddForce(transform.up * 7f,ForceMode2D.Impulse);
    }

    void Update()
    {
        if(transform.localPosition.y < -6)
        {
            
            HideFromStage();
        }
    }



    public void HideFromStage()
    {
        rb2d.simulated = false;
        rb2d.simulated = true;
        pool.Collect(this);
    }
}
