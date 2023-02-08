using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public StateMachine stateMachine;
    abstract public void OnStart(StateMachine fsm);
    abstract public void OnUpdate();
    abstract public void OnStateEnd();
    abstract public void OnCollision(Collider other);
}
