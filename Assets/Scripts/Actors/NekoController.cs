using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NekoController : MonoBehaviour
{

    Transform target;
    
    [SerializeField] float speed;
    CatPoolController nekoPool;
    CircleCollider2D cc2d;
    Animator anim;
    public bool isRush;

    public UnityAction HitAngryArea,StealTarget,LoopCount,RushStart;
    public enum STATE
    {
        STOP,
        WALK,
        DASH,
        ESCAPE,
        TURN,
        LOSE
    }
    public STATE state;

    private void Start()
    {
        anim = GetComponent<Animator>();
        nekoPool = transform.parent.GetComponent<CatPoolController>();
        gameObject.SetActive(false);
        cc2d = GetComponent<CircleCollider2D>();
        speed = Random.Range(1.5f, 3.5f);
        
    }

    public void Settarget()
    {
        
        state = STATE.WALK;
    }

    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
        if (isRush)
        {
            LoopCount?.Invoke();
        }
    }

    public void HideFromStage()
    {
        if (isRush)
        {
            RushStart?.Invoke();
        }
        nekoPool.Collect(this);
    }

    public void HideInRush()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {

        switch (state)
        {
            case STATE.WALK:
                transform.Translate(Vector3.down * speed * Time.deltaTime);
                break;

            case STATE.DASH:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 3);
                break;
            case STATE.ESCAPE:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 2);
                break;
            case STATE.TURN:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 2);
                break;
            case STATE.LOSE:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 0.1f);
                break;
        }

        if ((state == STATE.TURN || state == STATE.ESCAPE) && transform.position.y > 6f)
        {

            if (transform.GetComponentInChildren<TargetController>())
            {
                Destroy(transform.GetComponentInChildren<TargetController>().gameObject);
            }
            HideFromStage();
            

            
        }
    }




    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Area") )
        {
            float _tmpDistance = 0;
            int targetIdx = 0;
            TargetController[] targets = collision.GetComponentsInChildren<TargetController>();
            if(targets.Length <= 0 && state == STATE.WALK)
            {
                state = STATE.LOSE;
                anim.SetBool("isEscape", true);
                cc2d.enabled = false;
                return;
            }
            else if(state == STATE.WALK)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    float _distance = Vector3.Distance(targets[i].transform.position, transform.position);
                    if (i == 0)
                    {
                        _tmpDistance = _distance;
                    }
                    if (_tmpDistance > _distance)
                    {
                        targetIdx = i;
                        _tmpDistance = _distance;
                    }
                }
                target = targets[targetIdx].transform;
            }
                
            
            
            if(state == STATE.WALK)
            {
                state = STATE.DASH;
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            collision.transform.SetParent(transform);
            collision.GetComponent<TargetController>().OffCollider();
            StealTarget?.Invoke();
            state = STATE.TURN;
        }
        if (collision.CompareTag("AngerArea"))
        {
            state = STATE.ESCAPE;
            anim.SetBool("isEscape", true);
            HitAngryArea?.Invoke();
        }
    }

    public void StopNeko()
    {
        state = STATE.STOP;
    }

    public void EscapeNeko()
    {
        state = STATE.ESCAPE;
        anim.SetBool("isEscape", true);

    }



}
