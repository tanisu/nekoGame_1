using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusOyaji : MonoBehaviour
{
    enum DIRECTION
    {
        LEFT,
        RIGHT
    }
    DIRECTION direction;
    int _x;
    float speed = 1.5f;
    float xLimit = 1.4f;

    Coroutine cor;

    
    public void WalkStart()
    {
        cor = StartCoroutine(_walk());
    }

    public void EndWalk()
    {
        if (cor != null)
        {
            StopCoroutine(cor);
        }

        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }

    IEnumerator _walk()
    {
        
        while (true)
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


            if (transform.localPosition.x < -xLimit)
            {
                direction = DIRECTION.RIGHT;
            }
            else if (transform.localPosition.x > xLimit)
            {
                direction = DIRECTION.LEFT;
            }
            yield return null;
        }
        
    }
}
