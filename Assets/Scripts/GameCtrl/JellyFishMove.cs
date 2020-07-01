using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishMove : MonoBehaviour
{
    private float moveSpeed = 1f;
    private Vector2 direction = Vector2.down;

    private bool isDirectionChanged = true;

    private void Update()
    {
        Move(moveSpeed, direction);
    }

    void Move(float speed, Vector2 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (isDirectionChanged)
        {
            StartCoroutine(ChangeDirection(5));
        }
    }

    private IEnumerator ChangeDirection(float seconds)
    {
        isDirectionChanged = false;

        yield return new WaitForSeconds(seconds);

        if (direction == Vector2.up)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = Vector2.up;
        }

        isDirectionChanged = true;
    }
}
