using System.Collections;
using UnityEngine;

public class MedusaDgree : MonoBehaviour
{
    public GameObject enemy;
    private Transform player;
    private Vector3 randomPosition;

    private int [] radianSize = { 1, 2, 3 };
    private float _Size = 1.25f;
    [SerializeField] private float _Delay = 0.8f;
    public static bool _check;

    private void Start()
    {
        player = Managers.Game.Player.gameObject.transform;
        StartCoroutine(InstantAttack(GetRandomInt(radianSize)));
    }

    IEnumerator InstantAttack(int[] rand)
    {
        radianSize = rand;
        Vector2 target = new Vector2(player.position.x, player.position.y);
        for (int i = 0; i < 3; i++)
        {
            int circumference = (int)(2 * Mathf.PI * radianSize[i] / _Size);
            GetRandomPosition(target, circumference, radianSize[i]);

            yield return new WaitForSeconds(_Delay);
        }
    }

    public void GetRandomPosition(Vector2 _target, int circumference, int _radius)
    {
        for (int i = 0; i < 360; i += 360 / circumference)
        {
            float radius = _radius;

            float rad = i * Mathf.Deg2Rad;
            float x = _target.x + (radius * Mathf.Cos(rad));
            float y = _target.y + (radius * Mathf.Sin(rad));

            randomPosition = new Vector3(x, y, 0);

            Instantiate(enemy, randomPosition, Quaternion.identity);
        }
    }

    public int[] GetRandomInt(int[] array)
    {
        int random1, random2;
        int temp;

        for (int i = 0; i < array.Length; i++)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }
}
