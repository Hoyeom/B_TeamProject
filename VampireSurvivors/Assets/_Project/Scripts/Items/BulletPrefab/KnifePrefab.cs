using UnityEngine;

public class KnifePrefab : ProjectilePrefab
{
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
}