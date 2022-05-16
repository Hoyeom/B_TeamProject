using UnityEngine;
using _Project.Scripts.Enemy;

public abstract class FMBase : MonoBehaviour /*, IEnemy*/
{
    /// <summary>
    /// Test 해당 클레스 문제점
    /// 1.이름 및 번호지정이 의미가 없어짐
    /// </summary>
    private static int Final_MonsterID;
    private string monsterName;

    private int number;
    public int Number
    {
        get => number;
        set
        {
            number = value;
            Final_MonsterID++;
        }
    }

    public virtual void Initialize(string name)
    {
        Number = Final_MonsterID;
        monsterName = name;
    }

    public abstract void Updated();

    public void TestDebug(string txt) { Debug.Log($"{monsterName} : {txt}"); }
}
