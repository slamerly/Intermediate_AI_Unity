using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatState : BaseState
{
    StateMachine stateM;
    bool ready = false;
    float delay = 2f;

    public override void OnStart(StateMachine fsm)
    {
        stateM = fsm;
        Debug.Log("Go eat!");
    }
    public override void OnUpdate()
    {
        if (Vector3.Distance(stateM.transform.position, stateM.food.position) <= 3f)
        {
            stateM.agent.isStopped = true;
            if (!ready)
            {
                stateM.delay = delay;
                ready = true;
                Debug.Log("ready " + ready);
            }
            else if (stateM.delay <= 0)
            {
                stateM.food.localScale = Vector3.Lerp(stateM.food.localScale, Vector3.zero, Time.deltaTime);
            }
            if(stateM.food.localScale.x <= 0.01f)
                OnStateEnd();
        }
        else
        {
            stateM.agent.SetDestination(stateM.food.position);
        }
    }

    public override void OnStateEnd()
    {
        Debug.Log("I'm full");
        //stateM.OnStateEnd();
    }
}
