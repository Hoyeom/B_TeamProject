using _Project.Scripts.Enemy;
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

public class FMonster : FMBase, IEnemy
{
    [HideInInspector] public float CurrentTime;
    [HideInInspector] public float health;
    [HideInInspector] public bool _Test;
    [HideInInspector] public Rigidbody2D _rigid;
    [HideInInspector] public SpriteRenderer _renderer;
    [HideInInspector] public Animator _animator;
    [HideInInspector] public Transform AttackPos;

    [SerializeField] public FMSpecSO monsterSpec;

    private IState<FMonster>[] saveState;
    private StateMachine<FMonster> stateMachine;

    private void OnEnable() { MgrInfo(); }

    public override void Initialize(string name)
    {
        base.Initialize(name);
        gameObject.name = $"{name}_{Number:D2}_(Clone)";
        health = monsterSpec.MaxHealth;

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
        AttackPos = transform.Find("Attackpos")?.gameObject.transform;
    }

    public override void TakeDamage(float damage, Vector2 target)
    {
        if (health < 1) { return; }

        Managers.UI.SpawnDamageText((int)damage, transform.position);
        health -= damage;

        _rigid.MovePosition(_rigid.position + ((Vector2)transform.position - target) * 1 * Time.deltaTime);
        //Managers.Audio.FXEnemyAudioPlay(hitSoundClip);
        if (health < 1)
        {
            GameObject prefab = ObjectPooler.Instance.GenerateGameObject(monsterSpec.ExpPrefabs); // 임시
            prefab.transform.position = transform.position;
            prefab.GetComponent<Experience>().DropExp(monsterSpec.DropExp);
            //Managers.Game.Room.killMonsterCount++;
            FMonster entity = gameObject.GetComponent<FMonster>();
            MonsterContrl.fmonster(entity);
            Managers.Game.Room.killMonsterCount++;
            Destroy(gameObject);
            return;
        }
        //_animator.SetTrigger(hashHitAnim);
    }

    private void Attacks()
    {
        GameObject enemyArrow = ObjectPooler.Instance.GenerateGameObject(monsterSpec.Attackprefabs);
        enemyArrow.transform.position = AttackPos.position;

        Vector2 pos = AttackPos.transform.position - BossMonsterMgr.Inst._player.transform.position;
        float radian = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

        enemyArrow.transform.rotation = Quaternion.Euler(0, 0, radian);
    }

}