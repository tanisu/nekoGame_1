using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoController : MonoBehaviour
{

    Transform target;
    
    [SerializeField] float speed;
    //bool isExistTarget = true;
    //bool isTrun,isSetTarget;
    CircleCollider2D cc2d;

    enum STATE
    {
        WALK,
        DASH,
        ESCAPE,
        TURN
    }
    STATE state;

    private void Start()
    {
        cc2d = GetComponent<CircleCollider2D>();
        speed = Random.Range(1.5f, 3.5f);
    }

    public void Settarget(Transform _tf)
    {
        //target = _tf;
        state = STATE.WALK;
    }

    void Update()
    {

        switch (state)
        {
            case STATE.WALK:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;

            case STATE.DASH:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 2);
                break;
            case STATE.ESCAPE:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 2);
                break;
            case STATE.TURN:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 2);
                break;
        }

        if ((state == STATE.TURN || state == STATE.ESCAPE) && transform.position.y > 6f)
        {
            Destroy(gameObject);
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Area") )
        {
            AreaController _area = collision.GetComponent<AreaController>();

            target = collision.GetComponentInChildren<Transform>();
            Debug.Log(target);
            if(_area.state == AreaController.AREA_STATE.NORMAL && state != STATE.ESCAPE && state != STATE.TURN)
            {
                state = STATE.DASH;
            }
            else if (_area.state == AreaController.AREA_STATE.ANGRY)
            {
                state = STATE.ESCAPE;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            collision.transform.SetParent(transform);
            state = STATE.TURN;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Line"))
    //    {
    //        state = STATE.ESCAPE;
    //        cc2d.enabled = false;
    //        Destroy(collision.gameObject);
    //    }
    //}


}
