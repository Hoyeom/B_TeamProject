using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Test 나중에 추가
/// <summary>
/// 이동, 공격, 특수공격
/// </summary>
public enum States
{
    Monster_Move = 0,
    Monster_Attack,
    Monster_SpAttack,
}


public class Medusa : FMBase
{
    [HideInInspector] public Rigidbody2D _rigid;
    [HideInInspector] public SpriteRenderer _renderer;
    Animator _animator;

    //public GameEvent searchPlayer;

    [SerializeField] public FMSpecSO monsterSpec;

    private float CurrentHP;
    
    private IState<Medusa>[] saveState;
    private StateMachine<Medusa> stateMachine;
    
    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private void OnEnable()
    {
        //spec = Instantiate(monsterSpec);
        MgrInfo();
    }
    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public override void Initialize(string name)
    {
        base.Initialize(name);
        gameObject.name = $"{name}_{Number:D2}_(Clone)";

        StateInit();

        stateMachine = new StateMachine<Medusa>();
        stateMachine.Initialize(this, saveState[(int)States.Monster_Move]);

        CurrentHP = monsterSpec.MaxHealth;
    }



    public void StateInit()
    {
        // Test 추적, 공격, 특수공격 추가하기
        saveState = new IState<Medusa>[Enum.GetValues(typeof(States)).Length];
        saveState[(int)States.Monster_Move] = new MonsterStates.Monster_Move();
        saveState[(int)States.Monster_Attack] = new MonsterStates.Monster_Attack();
        saveState[(int)States.Monster_SpAttack] = new MonsterStates.Monster_SpAttack();
    }
    public override void Updated()
    {
        stateMachine.OnStateUpdate();
    }

    #region Test 상태 변경
    public void StateChange(States state)
    {
        stateMachine.StateChage(saveState[(int)state]);
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
    //~~~~~~~~~~~~~~ Test 테스트용 ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
