using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public UnityAction<int> GetBonus;
    Coroutine cor;

    [SerializeField] ParticleSystem heart;

    public void WalkStart()
    {
        cor = StartCoroutine(_walk());
    }

    IEnumerator _walk()
    {
        while(true)
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
            yield return null;
        }
    }

    public void EndWalk()
    {
        if(cor != null)
        {
            StopCoroutine(cor);
        }

        transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BonusFish"))
        {
            Instantiate(heart, new Vector3(transform.position.x, transform.position.y), transform.rotation);
            SoundController.I.PlaySE(SESoundData.SE.NEKO);
            BonusFishController _fish = collision.GetComponent<BonusFishController>();
            GetBonus?.Invoke(_fish.fishData.score);
            _fish.HideFromStage();
        }
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
}
