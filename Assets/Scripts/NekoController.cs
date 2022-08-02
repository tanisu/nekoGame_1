using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoController : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] float speed;
    bool isExistTarget = true;
    bool isTrun;
    CircleCollider2D cc2d;

    private void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
    }


    void Update()
    {
        if (isTrun)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime * 2);
        }
        else if( isExistTarget && (target.position - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position =  Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            isExistTarget = false;
            Destroy(collision.gameObject);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Line"))
        {
            isTrun = true;
            cc2d.enabled = false;
            Destroy(collision.gameObject);
        }
    }


}
