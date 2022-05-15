using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    public SpriteRenderer spr;
    public float speed = 2.0f;

    private void OnEnable()
    {
        spr.color = new Color(1, 1, 1, 1);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
        spr.color = Color.Lerp(spr.color, new Color(1, 1, 1, 0), Time.fixedDeltaTime);
    }
}
