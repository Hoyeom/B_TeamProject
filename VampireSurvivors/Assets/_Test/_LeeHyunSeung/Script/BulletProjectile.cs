using UnityEngine;

public class BulletProjectile : ProjectilePrefab
{
    private void FixedUpdate()
    {
        Debug.Log(speed);
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
}