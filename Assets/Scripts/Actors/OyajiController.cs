using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyajiController : MonoBehaviour
{
    [SerializeField] GameObject AngerArea;
    Animator anim;
    [SerializeField] float AngerLimit,xLimit,speed, tiredTime;
    float time,delInterval,angerInterval;
    bool isTired,isAngry;
    

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

        if (isTired)
        {
            StartCoroutine(_istied());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isAnger();
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isTired)
        {
            _coolDown();
        }
        if (Input.GetKey(KeyCode.Space)){
            _getAnger();
        }

        if(!isAngry && !isTired)
        {
            _walk();
        }
        
    }

    private void _walk()
    {
        switch (direction)
        {
            case DIRECTION.LEFT:
                _x = -1;
                break;
            case DIRECTION.RIGHT:
                _x = 1;
                break;
        }

        transform.localScale = new Vector3(_x, 1, 1);
        transform.Translate(new Vector3(_x, 0, 0) * Time.deltaTime * speed);


        if (transform.position.x < -xLimit)
        {
            direction = DIRECTION.RIGHT;
        }
        else if (transform.position.x > xLimit)
        {
            direction = DIRECTION.LEFT;
        }
    }

    private void _coolDown()
    {
        
        isAngry = false;
        AngerArea.SetActive(false);
        anim.SetBool("IsAnger", false);
        angerInterval = AngerLimit;
       
    }

    private void _getAnger()
    {
        delInterval -= Time.deltaTime;
        if (delInterval <= 0)
        {
            time += Time.deltaTime;
            angerInterval -= time;
            delInterval = 0.1f;
        }
        if (angerInterval < 0)
        {
            isAngry = false;
            isTired = true;
            AngerArea.SetActive(false);
            time = 0;
        }
    }

    private void _isAnger()
    {
        if (angerInterval >= 0)
        {
            isAngry = true;
            AngerArea.SetActive(true);
            anim.SetBool("IsAnger", true);
            anim.Play("Oyaji_Anger");
        }
        
    }


    IEnumerator _istied()
    {

        anim.SetBool("IsTired", true);
        anim.SetBool("IsAnger", false);
        
        yield return new WaitForSeconds(tiredTime);
        isTired = false;
        anim.SetBool("IsTired", false);
        angerInterval = AngerLimit;
    }



}
