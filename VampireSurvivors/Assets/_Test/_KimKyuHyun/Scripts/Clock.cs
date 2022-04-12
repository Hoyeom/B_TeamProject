using UnityEngine;

public class Clock: Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;
    private int angle;
    protected override void ActiveAttack(int i)
    {
        
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
       

        ClockPrefab stat = tempPrefab.GetComponent<ClockPrefab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.amount = GetMight();

        tempPrefab.transform.Rotate(0, 0, 30 * angle); // 방향 지정
        angle++;
    }

    protected override void Level2()
    {
        
    }

    protected override void Level3()
    {
        area += .3f;
        speed += .25f;
    }

    protected override void Level4()
    {
        duration += .5f;
       
    }

    protected override void Level5()
    {
        
    }

    protected override void Level6()
    {
        area += .3f;
        speed += .25f;
    }

    protected override void Level7()
    {
        duration += .5f;
  
    }

    protected override void Level8()
    {
        
        rarity = 0;
    }


}