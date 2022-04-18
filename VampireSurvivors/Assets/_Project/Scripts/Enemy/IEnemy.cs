using UnityEngine;

namespace _Project.Scripts.Enemy
{
    public interface IEnemy
    {
        public void TakeDamage(float damage, Vector2 target);
        public void SpeedSlow(float slow, float time);
    }
}