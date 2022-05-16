using System.Collections;
using UnityEngine;

/// <summary>
/// 필요 spec
/// 1. 공격 속도
/// 2. 돌진 종료 거리
/// 3. 돌진 속도
/// </summary>
namespace MonsterStates
{
    #region 이동
    public class Monster_Move : IState<FMonster>
    {
        float curSpeed;
        public void StateEnter(FMonster entity) 
        {
            curSpeed = entity.monsterSpec.MonsterSpeed;
        }
        public void StateUpdate(FMonster entity)
        {
            // 특수 공격
            entity.CurrentTime += Time.deltaTime;
            if (entity.CurrentTime >= entity.monsterSpec.CollTime) { entity.StateChange(States.Monster_SpAttack); }
            
            // 일반 공격
            if (entity.monsterSpec.AttackRange > Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position))
            {
                entity.StateChange(States.Monster_Attack);
            }

            // 실제 이동
            entity._rigid.MovePosition(entity._rigid.position +
                                (Vector2)((Vector2)BossMonsterMgr.Inst._player.transform.position - (Vector2)entity.transform.position).normalized * curSpeed * Time.deltaTime);
            entity._renderer.flipX = BossMonsterMgr.Inst._player.transform.position.x > entity.transform.position.x;
        }

        public void StateExit(FMonster entity) { }
    }
    #endregion

    #region 공격
    public class Monster_Attack : IState<FMonster>
    {
        bool first = false;
        float time;
        public void StateEnter(FMonster entity)
        {
            if (entity.monsterSpec.AttackBool)
            {
                if (!first)
                {
                    Attacks(entity);
                    first = true;
                }
            }
        }

        public void StateUpdate(FMonster entity)
        {
            time += Time.deltaTime;
            entity.CurrentTime += Time.deltaTime;

            // 원거리 전용
            if (entity.monsterSpec.AttackBool)
            {
                // 특수 공격 확인 - 판정 1순위, 사거리 제약 없음
                if(entity.CurrentTime >= entity.monsterSpec.CollTime) { entity.StateChange(States.Monster_SpAttack); }
                // 거리 비교
                else if (entity.monsterSpec.AttackRange < Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position))
                {
                    entity.StateChange(States.Monster_Move);
                }
                // 공속
                else if (time >= entity.monsterSpec.AttackSpeed)
                {
                    Attacks(entity);
                    time = 0;
                }
            }
            else  // 근거리 전용
            {
                BossMonsterMgr.Inst.anievents.RaiseEvent(entity, "Attack", AnimatorParameterSO.ParameterType.Trigger);
                entity.StateChange(States.Monster_Move);
            }

        }

        public void StateExit(FMonster entity) { }

        private void Attacks(FMonster entity)
        {
            GameObject enemyArrow = ObjectPooler.Instance.GenerateGameObject(entity.monsterSpec.Attackprefabs);
            enemyArrow.transform.position = entity.transform.position;

            Vector2 pos = entity.transform.position - BossMonsterMgr.Inst._player.transform.position;
            float radian = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

            enemyArrow.transform.rotation = Quaternion.Euler(0, 0, radian);
        }
    }
    #endregion

    // test 즉발과 지속 나눠야 하나?
    #region SpAttack
    public class Monster_SpAttack : IState<FMonster>
    {
        public GameEvent SpAttack;
        public void StateEnter(FMonster entity) { BossMonsterMgr.Inst.SpAttack.Raise(); }

        public void StateUpdate(FMonster entity)
        {
            if (!entity.name.Contains("Mantis")) { entity.StateChange(States.Monster_Move); }

            // 흠.... 이걸 어쩐다...
            else
            {
                // Test 숫자 직접넣음
                entity.transform.position = Vector2.Lerp(entity.transform.position, BossMonsterMgr.Inst._player.transform.position, Time.deltaTime * 3f);
                if (Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position) < 2f) { entity.StateChange(States.Monster_Move); }
            }
        }

        public void StateExit(FMonster entity) { entity.CurrentTime = 0; }
    }
    #endregion

}