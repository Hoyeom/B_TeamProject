using UnityEngine;

public class Bible : Item
{
    public GameObject attackPrefab;
    private GameObject tempPrefab;

    protected override void ActiveAttack(int i)
    {
        tempPrefab = ObjectPooler.Instance.GenerateGameObject(attackPrefab);
        tempPrefab.transform.position = transform.position; // 초기 위치 지정
        tempPrefab.transform.Translate(area * Mathf.Sin(Mathf.PI * 2 * i / amount), area * Mathf.Cos(Mathf.PI * 2 * i / amount), 0); // 개수로 360도를나눠서 생성

        BiblePrefab stat = tempPrefab.GetComponent<BiblePrefab>(); // 발사체 속도 데미지 지정
        stat.speed = GetSpeed();
        stat.amount = GetMight();
        stat.duration = GetDuration();
        stat.coolDown = GetCooldown();
    }

    protected override void Level2()
    {
        amount++;
    }

    protected override void Level3()
    {
        area += .3f;
        speed += .25f;
    }

    protected override void Level4()
    {
        duration += .5f;
        minMight += 10;
        maxMight += 10;
    }

    protected override void Level5()
    {
        amount++;
    }

    protected override void Level6()
    {
        area += .3f;
        speed += .25f;
    }

    protected override void Level7()
    {
        duration += .5f;
        minMight += 10;
        maxMight += 10;
    }

    protected override void Level8()
    {
        amount++;
        rarity = 0;
    }


}