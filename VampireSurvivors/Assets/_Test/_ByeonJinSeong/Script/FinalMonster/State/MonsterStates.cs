using System.Collections;
using UnityEngine;

namespace MonsterStates
{
    // Test 수정 가능성 매우 높음
    #region 이동
    public class Monster_Move : IState<FMonster>
    {
        float curSpeed;
        public void StateEnter(FMonster entity) { curSpeed = entity.monsterSpec.MonsterSpeed; }
        public void StateUpdate(FMonster entity)
        {
            entity.CurrentTime += Time.deltaTime;
            if (entity.CurrentTime >= entity.monsterSpec.CollTime)
            {
                entity.StateChange(States.Monster_SpAttack);
            }

            if (entity.monsterSpec.AttackBool)
            {
                if (entity.monsterSpec.AttackRange > Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position))
                {
                    entity.StateChange(States.Monster_Attack);
                }
            }
            // Test 근거리 공격모션 추가여부 고민
            // else

            entity._rigid.MovePosition(entity._rigid.position +
                                (Vector2)((Vector2)BossMonsterMgr.Inst._player.transform.position - (Vector2)entity.transform.position).normalized * curSpeed * Time.deltaTime);
            entity._renderer.flipX = BossMonsterMgr.Inst._player.transform.position.x > entity.transform.position.x;
        }

        public void StateExit(FMonster entity) { }
    }
    #endregion


    // Test 문제 상당히 많음
    /// <summary>
    /// 작업 목록
    /// 1. 근거리 OR 원거리 구분 처리
    /// 2. 원거리 시 공격속도(매직넘버+...)
    /// 3. 애니메이션 연결
    /// </summary>
    #region 공격
    public class Monster_Attack : IState<FMonster>
    {
        bool first = false;
        float time;
        public void StateEnter(FMonster entity)
        {
            if (!first)
            {
                Attacks(entity);
                first = true;
            }
        }
        public void StateUpdate(FMonster entity)
        {
            time += Time.deltaTime;
            entity.CurrentTime += Time.deltaTime;

            // 원거리 전용
            if (entity.monsterSpec.AttackRange < Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position))
            {
                entity.StateChange(States.Monster_Move);
            }
            else if(entity.CurrentTime >= entity.monsterSpec.CollTime)
            {
                entity.StateChange(States.Monster_SpAttack);
            }

            if (time >= 0.3f && entity.monsterSpec.AttackBool)
            {
                Attacks(entity);
                time = 0;
            }
        }

        public void StateExit(FMonster entity){}

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

    // Test 일단 1차 완료
    #region SpAttack
    public class Monster_SpAttack : IState<FMonster>
    {
        public GameEvent SpAttack;
        public void StateEnter(FMonster entity)
        {
            BossMonsterMgr.Inst.SpAttack.Raise();
        }
        public void StateUpdate(FMonster entity)
        {
            entity.StateChange(States.Monster_Move);
        }

        public void StateExit(FMonster entity)
        {
            entity.CurrentTime = 0;
        }
    }
    #endregion
}