using UnityEngine;

public abstract class FMBase : MonoBehaviour
{
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

    public void TestDebug(string txt)
    {
        Debug.Log($"{monsterName} : {txt}");
    }
}
