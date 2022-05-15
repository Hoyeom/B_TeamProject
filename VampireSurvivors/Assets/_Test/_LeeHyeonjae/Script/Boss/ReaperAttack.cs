using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAttack : MonoBehaviour
{
    private float speed = 250f;
    private float movement= 2f;
    private Rigidbody2D rigid;
    private float might = 10.0f;
    public Renderer rend;
    private Transform child;
    Coroutine MoveAction;

    private void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        child = transform.GetChild(0);
    }

    private void OnEnable()
    {
        Invoke("ScyOn", 1f);
        MoveAction = StartCoroutine(Move());
    }

    void ScyOn()
    {
        rend.enabled = true;
    }

   IEnumerator Move()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            transform.Translate(Vector2.right * movement * Time.fixedDeltaTime);
            child.Rotate(0, 0, -Time.fixedDeltaTime * speed);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDisable()
    {
        StopCoroutine(MoveAction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            other.gameObject.GetComponent<Player>().AttackChangeHealth(might);
            ObjectPooler.Instance.DestroyGameObject(this.gameObject);
        }
    }

}
