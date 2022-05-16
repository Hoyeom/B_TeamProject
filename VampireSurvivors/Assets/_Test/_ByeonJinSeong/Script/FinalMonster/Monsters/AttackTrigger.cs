using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag(gameObject.tag))
		{
			Debug.Log("맞았음!");
			// 일단 대기
		}
	}
}
