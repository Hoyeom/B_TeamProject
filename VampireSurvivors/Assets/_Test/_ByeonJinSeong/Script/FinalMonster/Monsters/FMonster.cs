using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    Monster_Move = 0,
    Monster_Attack,
    Monster_SpAttack,
    Monster_SpAttack_C,
}

public class FMonster : FMBase
{
    [HideInInspector] public float CurrentTime;
    [HideInInspector] public Rigidbody2D _rigid;
    [HideInInspector] public SpriteRenderer _renderer;
    [HideInInspector] public Animator _animator;

    [SerializeField] public FMSpecSO monsterSpec;

    private IState<FMonster>[] saveState;
    private StateMachine<FMonster> stateMachine;
    
    private void OnEnable() { MgrInfo(); }

    public override void Initialize(string name)
    {
        base.Initialize(name);
        gameObject.name = $"{name}_{Number:D2}_(Clone)";

        StateInit();

        stateMachine = new StateMachine<FMonster>();
        stateMachine.Initialize(this, saveState[(int)States.Monster_Move]);

    }

    public void StateInit()
    {
        saveState = new IState<FMonster>[Enum.GetValues(typeof(States)).Length];
        saveState[(int)States.Monster_Move] = new MonsterStates.Monster_Move();
        saveState[(int)States.Monster_Attack] = new MonsterStates.Monster_Attack();
        saveState[(int)States.Monster_SpAttack] = new MonsterStates.Monster_SpAttack();
        saveState[(int)States.Monster_SpAttack_C] = new MonsterStates.Monster_SpAttac_C();
    }
    public override void Updated() { stateMachine.OnStateUpdate(); }

    #region 상태 변경
    public void StateChange(States state) { stateMachine.StateChage(saveState[(int)state]); }
    #endregion

    public void MgrInfo()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
}