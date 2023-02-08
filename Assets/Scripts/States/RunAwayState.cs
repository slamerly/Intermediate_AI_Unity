using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class RunAwayState : BaseState
{
    bool ready = false;
    float delay = 2f;
    float runSpeed = 2;
    float saveSpeed = 0;

    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        Debug.Log("Go! Go! Go!");
        saveSpeed = stateMachine.agent.speed;
        stateMachine.agent.speed = saveSpeed * runSpeed;
        stateMachine.agent.SetDestination(stateMachine.bed.position);
    }
    public override void OnUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.bed.position) <= 1f)
        {
            if (!ready)
            {
                stateMachine.delay = delay;
                ready = true;
            }
            else if (stateMachine.delay <= 0)
            {
                OnStateEnd();
            }
        }
    }

    public override void OnStateEnd()
    {
        stateMachine.textUI.SetText("Cat: I'm safe here.");
        Debug.Log("I'm safe here.");
        stateMachine.agent.speed = saveSpeed;

        stateMachine.seeDog = false;
        stateMachine.needLitter = true;

        stateMachine.OnStateEnd();
    }

    public override void OnCollision(Collider other)
    {
    }
}
