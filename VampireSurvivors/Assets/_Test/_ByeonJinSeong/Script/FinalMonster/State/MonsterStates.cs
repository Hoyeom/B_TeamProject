using System.Collections;
using UnityEngine;

namespace MonsterStates
{
    // Medusa용 V_날 먹
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
            entity.CurrentTime += Time.deltaTime;
            // Test 우럭 회
            if (entity.CurrentTime >= entity.monsterSpec.CollTime)
            {
                // 시간 비교 후 특수공격으로
                entity.StateChange(States.Monster_SpAttack);
            }

            // Test 날먹
            if (entity.monsterSpec.AttackRange > Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position))
            {
                entity.StateChange(States.Monster_Attack);
            }

            /*
            * Test 광어
            */
            entity._rigid.MovePosition(entity._rigid.position +
                                (Vector2)((Vector2)BossMonsterMgr.Inst._player.transform.position - (Vector2)entity.transform.position).normalized * curSpeed * Time.deltaTime);
            entity._renderer.flipX = BossMonsterMgr.Inst._player.transform.position.x > entity.transform.position.x;
        }

        public void StateExit(FMonster entity)
        {
        }

    }
    #endregion
    #region 공격
    public class Monster_Attack : IState<FMonster>
    {
        // Test 날먹
        bool first = true;
        float time;
        public void StateEnter(FMonster entity)
        {
            if (first)
            {
                Attacks(entity);
                first = false;
            }
        }
        public void StateUpdate(FMonster entity)
        {
            time += Time.deltaTime;
            entity.CurrentTime += Time.deltaTime;
            // Test 활어 회
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
            else
            {
                // 예정
                
            }
        }

        public void StateExit(FMonster entity)
        {
        }

        private void Attacks(FMonster entity)
        {
            GameObject enemyArrow = ObjectPooler.Instance.GenerateGameObject(entity.monsterSpec.Attackprefabs);
            enemyArrow.transform.position = entity.transform.position;

            Vector2 pos = entity.transform.position - BossMonsterMgr.Inst._player.transform.position;
            float rad = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

            enemyArrow.transform.rotation = Quaternion.Euler(0, 0, rad);
        }
    }
    #endregion

    #region 날먹 집합소
    public class Monster_SpAttack : IState<FMonster>
    {
        public void StateEnter(FMonster entity)
        {
            entity.TestDebug("SpAttack 상태 진입");
            entity.TestDebug("SpAttack 공격");
        }
        public void StateUpdate(FMonster entity)
        {
            entity.StateChange(States.Monster_Move);
        }

        public void StateExit(FMonster entity)
        {
            entity.TestDebug("SpAttack 상태 완료 후 종료");
            entity.CurrentTime = 0;
        }
    }
    #endregion

    
}