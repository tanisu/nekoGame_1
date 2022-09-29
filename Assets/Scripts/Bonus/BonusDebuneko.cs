using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusDebuneko : MonoBehaviour
{
    enum DIRECTION
    {
        LEFT,
        RIGHT
    }
    DIRECTION direction;
    int _x;
    float xLimit = 1.5f;
    float speed = 0.9f;

    // Update is called once per frame
    void Update()
    {
        _walk();
        
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

        transform.localScale = new Vector3(-_x, 1, 1);
        transform.Translate(new Vector3(_x, 0, 0) * Time.deltaTime * speed);


        if (transform.localPosition.x < -xLimit)
        {
            direction = DIRECTION.RIGHT;
        }
        else if (transform.localPosition.x > xLimit)
        {
            direction = DIRECTION.LEFT;
        }
    }


}
