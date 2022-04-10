using UnityEngine;

public class BulletProjectile : ProjectilePrefab
{
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime, Space.Self);
    }
}