using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusNeko : MonoBehaviour
{
    enum DIRECTION
    {
        LEFT,
        RIGHT
    }
    DIRECTION direction;
    int _x;
    float speed = 0.8f;
    float xLimit = 1.73f;




    void Update()
    {
        _walk();
    }


    void _walk()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BonusFish"))
        {
            SoundController.I.PlaySE(SESoundData.SE.NEKO);
            collision.GetComponent<BonusFishController>().HideFromStage();
        }
    }
}
