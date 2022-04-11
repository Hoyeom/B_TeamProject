using UnityEngine;

public class BulletProjectile : ProjectilePrefab
{
    private void FixedUpdate()
    {
        Debug.Log("총알");
        Debug.Log(speed);
        speed = 3;
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }
}