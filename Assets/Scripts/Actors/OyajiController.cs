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
    bool isTired,isAngry, isOver,isPressed,isHighSpeed,isStop;
    Coroutine cor;
    SpriteRenderer sp;
    
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
        sp = GetComponent<SpriteRenderer>();
        
        delInterval = 0.1f;
    }


    public void OyajiInit()
    {
        transform.position = new Vector2(0f, transform.position.y);
       
    }




    public void OyajiWalk()
    {
        if (isStop) return;
        if ( gameObject.activeSelf && isTired)
        {
            cor = StartCoroutine(_istied());
        }

        


        if (isPressed && !isHighSpeed)
        {
            _getAnger();
        }

        if (!isAngry && !isTired && !isOver)
        {
            _walk();
        }
    }


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
        if (angerInterval >= 0 && !isTired && !isStop)
        {
  
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
        isHighSpeed = false;
        isStop = false;
        
        speed = 1f;
        isTired = false;
        isAngry = false;
        isPressed = false;
        sp.enabled = true;
        sp.color = new Color(1f,1f,1f,1f);
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

    public void TurnMe()
    {
        switch (direction)
        {
            case DIRECTION.LEFT:
                direction = DIRECTION.RIGHT;
                break;
            case DIRECTION.RIGHT:
                direction = DIRECTION.LEFT;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drink") && !isTired && !isStop)
        {
            
            SoundController.I.PlaySE(SESoundData.SE.DRINK);
            cor = StartCoroutine(_speedUp());
            collision.GetComponent<ItemController>().ItemCollect();
        }

        if(collision.CompareTag("Bom") && !isTired && !isStop)
        {
            SoundController.I.PlaySE(SESoundData.SE.BOM);
            
            BomController _bomcon = collision.GetComponent<BomController>();
            _bomcon.Explode(transform.position);
            
            cor = StartCoroutine(_bom());

        }
    }

    IEnumerator _bom()
    {
        isStop = true;
        int i = 0;
        while (i < 26)
        {
            sp.enabled = !sp.enabled;
            yield return new WaitForSeconds(0.1f);
            i++;
        }
        isStop = false;

    }


    IEnumerator _speedUp()
    {
        speed = 2.5f;
        isHighSpeed = true;
        sp.color = Color.red;
        yield return new WaitForSeconds(4f);
        isHighSpeed = false;
        sp.color = Color.white;
        speed = 1f;
    }

}
