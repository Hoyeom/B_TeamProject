using System.Collections;
using UnityEngine;

namespace MonsterStates
{
    #region 이동
    public class Monster_Move : IState<FMonster>
    {
        float curSpeed;
        public void StateEnter(FMonster entity) { curSpeed = entity.monsterSpec.MonsterSpeed; }
        public void StateUpdate(FMonster entity)
        {
            // 특수 공격
            entity.CurrentTime += Time.deltaTime;
            if (entity.CurrentTime >= entity.monsterSpec.CollTime) { entity.StateChange(States.Monster_SpAttack); }
            
            // 일반 공격
            if (entity.monsterSpec.AttackRange > Vector3.SqrMagnitude(entity.transform.position - Managers.Game.Player.transform.position))
            {
                entity.StateChange(States.Monster_Attack);
            }

            // 실제 이동
            entity._rigid.MovePosition(entity._rigid.position +
                                (Vector2)((Vector2)Managers.Game.Player.transform.position - (Vector2)entity.transform.position).normalized * curSpeed * Time.deltaTime);
            entity._renderer.flipX = Managers.Game.Player.transform.position.x > entity.transform.position.x;
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
                    Managers.Boss.anievents.RaiseEvent(entity, "Attack", AnimatorParameterSO.ParameterType.Trigger);
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
                else if (entity.monsterSpec.AttackRange < Vector3.SqrMagnitude(entity.transform.position - Managers.Game.Player.transform.position))
                {
                    entity.StateChange(States.Monster_Move);
                }
                // 공속
                else if (time >= entity.monsterSpec.AttackSpeed)
                {
                    Managers.Boss.anievents.RaiseEvent(entity, "Attack", AnimatorParameterSO.ParameterType.Trigger);
                    time = 0;
                }
            }
            else  // 근거리 전용
            {
                Managers.Boss.anievents.RaiseEvent(entity, "Attack", AnimatorParameterSO.ParameterType.Trigger);
                entity.StateChange(States.Monster_Move);
            }

        }

        public void StateExit(FMonster entity) { }

        
    }
    #endregion

    #region SpAttack
    public class Monster_SpAttack : IState<FMonster>
    {
        public GameEvent SpAttack;
        public void StateEnter(FMonster entity) 
        {
            // test 추 후 변경
            if (entity.name.Contains("Mantis") || entity.name.Contains("Alien")) { entity.StateChange(States.Monster_SpAttack_C); }
            Managers.Boss.SpAttack.Raise();
        }

        public void StateUpdate(FMonster entity) { entity.StateChange(States.Monster_Move); }

        public void StateExit(FMonster entity) { entity.CurrentTime = 0; }
    }
    #endregion

    public class Monster_SpAttac_C : IState<FMonster>
    {
        float duration;
        public void StateEnter(FMonster entity)
        {
            duration = entity.monsterSpec.CollTime;
            entity._Test = !entity._Test;
            if (entity.name.Contains("Alien"))
            {
                Managers.Boss.SpAttack.Raise();
                entity.StateChange(States.Monster_Move);
            }
        }

        public void StateUpdate(FMonster entity)
        {
            duration -= Time.deltaTime;

            if (duration <= 0) { entity.StateChange(States.Monster_Move); }
            Managers.Boss.SpAttack.Raise();
        }

        public void StateExit(FMonster entity) {  }
    }
}