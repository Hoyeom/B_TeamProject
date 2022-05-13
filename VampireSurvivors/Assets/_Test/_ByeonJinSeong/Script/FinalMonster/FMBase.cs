using UnityEngine;

public abstract class FMBase : MonoBehaviour
{
    /// <summary>
    /// Test 해당 클레스 문제점
    /// 1.이름 및 번호지정이 의미가 없어짐
    /// 2-1. Enemy를 상속받지 못함 다시 만들어야함 - 의존성 문제 + State변경 오류 발생 가능성 있음
    /// 2-2. Best 일반 몬스터와 보스 몬스터가 상속을 받음 >> 귀찮음
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
