using _Project.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
	[SerializeField] public FMSpecSO monsterSpec;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag(gameObject.tag))
		{
			foreach (var collider in Physics2D.OverlapCircleAll(transform.position, 2f, monsterSpec.TarGetLayer))
			{
				if (collider.TryGetComponent<IAttackable>(out IAttackable attackable))
				{
					attackable.AttackChangeHealth(monsterSpec.Damage);
				}
			}
		}
	}
}
