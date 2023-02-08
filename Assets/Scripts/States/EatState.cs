using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatState : BaseState
{
    bool ready = false;
    float delay = 2f;

    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        stateMachine.textUI.SetText("Cat: Go eat!");
        Debug.Log("Go eat!");
    }
    public override void OnUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.food.position) <= 2.5f)
        {
            stateMachine.agent.isStopped = true;
            if (!ready)
            {
                stateMachine.delay = delay;
                ready = true;
            }
            else if (stateMachine.delay <= 0)
            {
                stateMachine.food.localScale = Vector3.Lerp(stateMachine.food.localScale, Vector3.zero, Time.deltaTime);
            }
            if(stateMachine.food.localScale.x <= 0.01f)
                OnStateEnd();
        }
        else
        {
            stateMachine.agent.SetDestination(stateMachine.food.position);
        }
    }

    public override void OnStateEnd()
    {
        stateMachine.agent.isStopped = false;
        stateMachine.isHungry = false;
        stateMachine.needLitter = true;
        stateMachine.OnStateEnd();
    }

    public override void OnCollision(Collider other)
    {
    }
}
