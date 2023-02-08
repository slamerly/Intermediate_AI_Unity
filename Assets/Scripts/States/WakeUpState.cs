using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WakeUpState : BaseState
{
    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        stateMachine.isTired = false;
        stateMachine.textUI.SetText("Cat: Wake up!");
        Debug.Log("Wake up!");
        fsm.delay = 2;
    }

    public override void OnUpdate()
    {
        OnStateEnd();
    }

    public override void OnStateEnd()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            stateMachine.sleepOver = false;
            stateMachine.isHungry = true;
        }
        else
        {
            stateMachine.sleepOver = false;
            stateMachine.isWalking = true;
        }
        stateMachine.OnStateEnd();

    }

    public override void OnCollision(Collider other)
    {
    }
}
