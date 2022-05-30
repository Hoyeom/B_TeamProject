using _Project.Scripts.Player;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
	[SerializeField] public FMSpecSO monsterSpec;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag(gameObject.tag))
		{
			foreach (var collider in Physics2D.OverlapCircleAll(transform.position, monsterSpec.AttackRange, monsterSpec.TarGetLayer))
			{
				if (collider.TryGetComponent<IAttackable>(out IAttackable attackable))
				{
					attackable.AttackChangeHealth(monsterSpec.Damage);
				}
			}
		}
	}
}
