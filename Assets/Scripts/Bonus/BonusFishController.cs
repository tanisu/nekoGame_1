using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFishController : MonoBehaviour
{
    BonusPoolController pool;
    Rigidbody2D rb2d;

    void Start()
    {
        pool = transform.parent.GetComponent<BonusPoolController>();
        rb2d = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(transform.localPosition.y < -6)
        {
            
            HideFromStage();
        }
    }

    public void OnStage()
    {
        gameObject.SetActive(true);
        //rb2d.simulated = true;
       // rb2d.AddForce(transform.up * 1f);
    }

    public void HideFromStage()
    {
        rb2d.simulated = false;
        rb2d.simulated = true;
        pool.Collect(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BonusDebuneko"))
        {
            HideFromStage();
        }
    }
}
