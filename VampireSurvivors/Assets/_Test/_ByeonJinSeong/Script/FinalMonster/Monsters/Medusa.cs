using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test 나중에 추가
public enum States
{
    Monster_Idle = 0,
}
public class Medusa : FMBase
{
    Rigidbody2D _rigid;
    SpriteRenderer _renderer;
    Animator _animator;

    float curSpeed = 4f; // Test 임시 변수


    [SerializeField] private FMSpecSO monsterSpec;

    /*
     * Test 현재 상태 저장 변수
     * 처리 방법 고민
     */
    private float CurrentHP;

    private IState<Medusa>[] saveState;
    private StateMachine<Medusa> currentState;
    
    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void OnEnable()
    {
        MgrInfo();
    }
    void Start()
    {
        StartCoroutine(Move());
    }
    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public override void Initialize(string name)
    {
        base.Initialize(name);
        gameObject.name = $"{name}_{Number:D2}_(Clone)";

        StateInit();

        currentState = new StateMachine<Medusa>();
        currentState.Initialize(this, saveState[(int)States.Monster_Idle]);

        CurrentHP = monsterSpec.MaxHealth;
    }


    public override void Updated()
    {
        TestDebug("Test 확인");  // Test
        currentState.OnStateUpdate();
    }

    public void StateInit()
    {
        // Test 대기, 사망, 추적, 공격, 특수공격 추가하기
        saveState = new IState<Medusa>[5];
        saveState[(int)States.Monster_Idle] = new MonsterStates.Monster_Idle();
    }

    #region Test 상태 변경
    public void StateChange(States state)
    {
        currentState.StateChage(saveState[(int)state]);
    }
    #endregion


    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // Test 테스트용 나중에 Move 상태 만들어서 옮기기
    public void MgrInfo()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    IEnumerator Move()
    {
        while (true)
        {
            Vector2 pos = transform.position;
            Vector2 playerPos = BossMonsterMgr.Inst._player.Player.transform.position;

            _rigid.MovePosition(_rigid.position +
                               (Vector2)(playerPos - pos).normalized * curSpeed * Time.deltaTime);
            _renderer.flipX = playerPos.x > pos.x;

            //PlayerSearch();

            yield return new WaitForFixedUpdate();
            _rigid.velocity = Vector2.zero;
        }
    }
    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
