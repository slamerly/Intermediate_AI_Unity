using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WakeUpState : BaseState
{
    StateMachine stateM;
    public override void OnStart(StateMachine fsm)
    {
        stateM = fsm;
        stateM.isTired = false;
        Debug.Log("Wake up!");
        fsm.delay = 2;
    }

    public override void OnUpdate()
    {
        OnStateEnd();
    }

    public override void OnStateEnd()
    {
        int rand = Random.Range(1, 2);

        if (rand == 0)
        {
            stateM.isHungry = true;
        }
        else
            stateM.isHungry = false;
        Debug.Log("Hungry: "+  stateM.isHungry);
        stateM.OnStateEnd();

    }
}
