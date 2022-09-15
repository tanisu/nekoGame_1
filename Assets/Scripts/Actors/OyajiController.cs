using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OyajiController : MonoBehaviour
{
    [SerializeField] GameObject AngerArea,MaskArea;
    [SerializeField] Button KoraButton;
    Animator anim;
    [SerializeField] float AngerLimit,xLimit,speed, tiredTime;
    float time,delInterval,angerInterval;
    bool isTired,isAngry, isOver,isPressed;
    Coroutine cor;
    Vector2 firstPos, secondPos, currentPos;
    enum DIRECTION
    {
        LEFT,
        RIGHT
    }
    DIRECTION direction;
    int _x,touchCount;
    



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
            cor =  StartCoroutine(_istied());
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            _isAnger();
        }
        if (Input.GetKeyUp(KeyCode.Space) && !isTired)
        {
            _coolDown();
        }
        if (Input.GetKey(KeyCode.Space) || isPressed)
        {
            _getAnger();
        }

        */


        if (Input.GetMouseButtonDown(0))
        {
            _isAnger();
           // touchCount++;
           // Invoke("_isDash", 0.3f);

        }
        if(Input.GetMouseButton(0) || isPressed)
        {
            _getAnger();
        }
        if(Input.GetMouseButtonUp(0) && !isTired)
        {
            _coolDown();
            
        }

        if(!isAngry && !isTired && !isOver)
        {
            _walk();
        }
        
    }


    //void _isDash()
    //{
    //    //Debug.Log(touchCount);
    //    if(touchCount != 4) { touchCount = 0;return; }
    //    else { touchCount = 0; }
    //    StartCoroutine(_dash());
    //}

    //IEnumerator _dash()
    //{
    //    float beforeSpeed = speed;
    //    speed *= 2f;
    //    yield return new WaitForSeconds(0.5f);
    //    speed = beforeSpeed;
    //}

    public void OyajiLoose()
    {
        isOver = true;
        transform.Rotate(new Vector3(0,0,90f));
    }

    public void SwitchMask(bool _flg)
    {
        MaskArea.SetActive(_flg);
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

    public void _coolDown()
    {
        isAngry = false;
        isPressed = false;
        AngerArea.SetActive(false);
        anim.SetBool("IsAnger", false);
        angerInterval = AngerLimit;
        secondPos = Input.mousePosition;
        currentPos = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
        currentPos.Normalize();
        Debug.Log(currentPos);

        if(currentPos.x < 0)
        {
            direction = DIRECTION.LEFT;
        }
        else if(currentPos.x > 0)
        {
            direction = DIRECTION.RIGHT;
        }

        //direction = currentPos.x < -0.8f ? DIRECTION.LEFT : direction;
        //direction = currentPos.x > 0.8f ? DIRECTION.RIGHT : direction;

    }



    public void _getAnger()
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

    public void _isAnger()
    {
        if (angerInterval >= 0 && !isTired)
        {
            firstPos = Input.mousePosition;
            touchCount++;
            SoundController.I.PlaySE(SESoundData.SE.KORA);
            isAngry = true;
            isPressed = true;
            AngerArea.SetActive(true);
            anim.SetBool("IsAnger", true);
            anim.Play("Oyaji_Anger");
        }
        
    }





    public void ResetOyaji()
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }
        isTired = false;
        isAngry = false;
        isPressed = false;
        angerInterval = AngerLimit;
        AngerArea.SetActive(false);

    }



    IEnumerator _istied()
    {

        anim.SetBool("IsTired", true);
        anim.SetBool("IsAnger", false);
        
        yield return new WaitForSeconds(tiredTime);
        isTired = false;
        isPressed = false;
        anim.SetBool("IsTired", false);
        angerInterval = AngerLimit;
    }



}
