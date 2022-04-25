namespace MonsterStates
{
    // Medusa용 
    // Test 대기, 탐색, 공격, 특수 공격 흠.... 고민해보고 추가하기
    #region 몬스터의 대기 상태
    public class Monster_Idle : IState<Medusa>
    {
        public void StateEnter(Medusa entity)
        {
            entity.TestDebug("Idle 상태 진입");
        }
        public void StateUpdate(Medusa entity)
        {
            entity.TestDebug("Idle 상태 진행 중");
        }

        public void StateExit(Medusa entity)
        {
            entity.TestDebug("Idle 상태 완료 후 종료");
        }
    }
    #endregion
}
