public interface IState<T> where T : class
{
    public void StateEnter(T entity);
    public void StateUpdate(T entity);
    public void StateExit(T entity);
}