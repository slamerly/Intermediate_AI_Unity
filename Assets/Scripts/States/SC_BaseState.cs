public abstract class SC_BaseState
{
    abstract public void OnStart(SC_StateMachine fsm);
    abstract public void OnUpdate();
    abstract public void OnStateEnd();
}
