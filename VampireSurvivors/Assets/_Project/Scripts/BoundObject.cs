using UnityEngine;

public class BoundObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        ObjectPooler.Instance.DestroyGameObject(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ObjectPooler.Instance.DestroyGameObject(col.gameObject);
    }
}