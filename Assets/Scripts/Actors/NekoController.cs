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
    NekoData nekoData;
    public bool isLastCat;
    public bool isWait;
    public int nekoDataIndex,score;

    public UnityAction StealTarget,LoopCount;
    public UnityAction<int> HitAngryArea;
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


    public void NekoInit(NekoData _nekoData)
    {
        nekoData = _nekoData;
        anim = GetComponent<Animator>();
        nekoPool = transform.parent.GetComponent<CatPoolController>();
        gameObject.SetActive(false);
        cc2d = GetComponent<CircleCollider2D>();
        speed = nekoData.maxSpeed;
        score = _nekoData.score;
    }


    public void Settarget()
    {
        
        state = STATE.WALK;
    }

    public void ShowInStage(Vector3 _pos)
    {
        transform.position = _pos;
        cc2d.enabled = true;
        
        if (isLastCat)
        {
            
            LoopCount?.Invoke();
        }
    }

    public void HideFromStage()
    {
        nekoPool.Collect(this);
    }

    public void HideInRush()
    {
        gameObject.SetActive(false);
    }

    public void SpeedUp()
    {
        speed += 0.5f;
    }

    IEnumerator _stopAndGo()
    {
        
        yield return new WaitForSeconds(0.5f);
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        isWait = false;

    }

    void Update()
    {

        switch (state)
        {
            case STATE.WALK:

                _walk();
                break;

            case STATE.DASH:
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime * 3);
                break;
            case STATE.ESCAPE:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 3);
                break;
            case STATE.TURN:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 3);
                break;
            case STATE.LOSE:
                transform.Translate(Vector3.up * speed * Time.deltaTime * 0.1f);
                break;
        }

        if(state == STATE.DASH && !target.gameObject.activeSelf)
        {
            state = STATE.TURN;
        }

        if (
             ((state == STATE.TURN || state == STATE.ESCAPE || state == STATE.DASH) && transform.position.y > 5.2f) 
            || transform.position.y > 7f 
            || transform.position.x > 3f 
            || transform.position.x < -3f 
            || transform.position.y < -5.5f)
        {

            if (transform.GetComponentInChildren<TargetController>())
            {
                transform.GetComponentInChildren<TargetController>().gameObject.SetActive(false);
            }
            HideFromStage();
        }

    }


    void _walk()
    {
        if (nekoData.NekoType == NEKO_TYPE.STRAIGHT)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        if (nekoData.NekoType == NEKO_TYPE.WAVE)
        {
            transform.position = new Vector3(Mathf.Sin(Time.time * speed) * 0.45f, transform.position.y - (speed * Time.deltaTime), 0);
        }
        if (nekoData.NekoType == NEKO_TYPE.STOPANDGO)
        {
            if (transform.localPosition.y > -0.6f)
            {
                isWait = true;
                transform.Translate(Vector3.down * speed * Time.deltaTime);
            }
            else if (isWait)
            {
                StartCoroutine(_stopAndGo());
            }
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
            cc2d.enabled = false;
            StealTarget?.Invoke();
            
            state = STATE.TURN;
        }
        if (collision.CompareTag("AngerArea"))
        {
            state = STATE.ESCAPE;
            anim.SetBool("isEscape", true);
            cc2d.enabled = false;
            HitAngryArea?.Invoke(score);
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
