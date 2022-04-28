using System.Collections;
using UnityEngine;

namespace MonsterStates
{
    // Medusa용 V_날 먹
    // Memo 내가 이럴려고 상태를 3개나 만들었나 자괴감이 들어
    #region 이동
    public class Monster_Move : IState<Medusa>
    {
        float curSpeed;
        public void StateEnter(Medusa entity)
        {
            entity.TestDebug("Move 상태 진입");
            curSpeed = entity.monsterSpec.MonsterSpeed;
        }
        public void StateUpdate(Medusa entity)
        {
            // Test 우럭 회
            if (false)
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

        public void StateExit(Medusa entity)
        {
            entity.TestDebug("Move 상태 완료 후 종료");
        }

    }
    #endregion

    #region 공격
    public class Monster_Attack : IState<Medusa>
    {
        public void StateEnter(Medusa entity)
        {
            entity.TestDebug("Attack 상태 진입");
        }
        public void StateUpdate(Medusa entity)
        {
            // Test 활어 회
            if (entity.monsterSpec.AttackRange < Vector3.SqrMagnitude(entity.transform.position - BossMonsterMgr.Inst._player.transform.position))
            {
                entity.StateChange(States.Monster_Move);
            }

            entity.TestDebug("공격 중 암튼 공격 중");
        }

        public void StateExit(Medusa entity)
        {
            entity.TestDebug("Attack 상태 완료 후 종료");
        }
    }
    #endregion

    #region 날먹 집합소
    public class Monster_SpAttack : IState<Medusa>
    {
        public void StateEnter(Medusa entity)
        {
            entity.TestDebug("SpAttack 상태 진입");
        }
        public void StateUpdate(Medusa entity)
        {
            entity.TestDebug("공격 중 암튼 공격 중");
        }

        public void StateExit(Medusa entity)
        {
            entity.TestDebug("SpAttack 상태 완료 후 종료");
        }
    }
    #endregion
}