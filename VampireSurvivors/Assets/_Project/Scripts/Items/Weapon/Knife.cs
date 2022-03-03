using UnityEngine;

public class Knife : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;
    
    protected override void ActiveAttack(int i)
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position;                                     // 초기 위치 지정
        tempPrefab.transform.Translate(Vector2.one * Random.Range(-.2f,.2f));  // 위치 지정
        tempPrefab.transform.rotation = player.viewRotation;                                    // 방향 지정
        
        ProjectilePrefab stat = tempPrefab.GetComponent<ProjectilePrefab>();                    // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.amount = GetMight();
    }
}
