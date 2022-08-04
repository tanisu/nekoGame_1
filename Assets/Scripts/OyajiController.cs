using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyajiController : MonoBehaviour
{
    [SerializeField] GameObject AngerArea;
    Animator anim;
    [SerializeField] float AngerLimit,xLimit,speed;
    float time,delInterval,angerInterval;
    
    
    enum OYAJI_STATE
    {
        WALK,
        ANGER,
        TIERD
    }
    OYAJI_STATE state;

    enum DIRECTION
    {
        LEFT,
        RIGHT
    }
    DIRECTION direction;
    int _x;
    

    private void Start()
    {
        angerInterval = AngerLimit;
        anim = GetComponent<Animator>();
        delInterval = 0.1f;
    }

    private void Update()
    {

        if(state == OYAJI_STATE.TIERD)
        {
            StartCoroutine(_istied());
            return;
        }

        if(state == OYAJI_STATE.WALK)
        {
            switch (direction)
            {
                case DIRECTION.LEFT:
                    _x = -1;
                    break;
                case DIRECTION.RIGHT:
                    _x = 1;
                    break;
            }Å@

            transform.localScale = new Vector3(-_x, 1, 1);
            transform.Translate(new Vector3(_x, 0, 0) * Time.deltaTime * speed);

            
            if(transform.position.x < -xLimit)
            {
                direction = DIRECTION.RIGHT;
            }else if (transform.position.x > xLimit)
            {
                direction = DIRECTION.LEFT;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(angerInterval >= 0) {
                AngerArea.SetActive(true);
                anim.SetBool("IsAnger",true);
                state = OYAJI_STATE.ANGER;
            }
            
            
        }
        if (Input.GetKey(KeyCode.Space))
        {
            
            delInterval -= Time.deltaTime;
            if(delInterval <= 0)
            {
                time += Time.deltaTime;
                angerInterval -= time;
                delInterval = 0.1f;
            }
            if(angerInterval < 0)
            {
                state = OYAJI_STATE.TIERD;
                AngerArea.SetActive(false);
            }
            
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            AngerArea.SetActive(false);
            anim.SetBool("IsAnger",false);
            angerInterval = AngerLimit;
            state = OYAJI_STATE.WALK;

        }
    }

    IEnumerator _istied()
    {
        anim.SetBool("IsAnger",false);
        yield return new WaitForSeconds(1f);
        state = OYAJI_STATE.WALK;
        angerInterval = AngerLimit;

    }



}
