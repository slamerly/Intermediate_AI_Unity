using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : BaseState
{
    int current = 0;
    bool ready = false;
    float delay = 2f;

    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        stateMachine.textUI.SetText("Cat: Go walk around the house!");
        Debug.Log("Go walk around the house!");
        MoveToNextTaget();
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.wayToWalk[current].position) <= 1f)
        {
            if(!ready)
            {
                ready = true;
                stateMachine.delay = delay;
            }
            else if (stateMachine.delay <= 0)
            {
                current = (current + 1) % stateMachine.wayToWalk.Count;
                ready = false;
                OnStateEnd();
            }
        }
    }

    public override void OnStateEnd()
    {
        int rand = Random.Range(0, 3);

        switch(rand)
        {
            // Continue
            case 0:
                Debug.Log("No one here.");
                MoveToNextTaget();
                break;
            // Hungry
            case 1:
                stateMachine.isWalking = false;
                stateMachine.isHungry = true;
                stateMachine.textUI.SetText("Cat: I'm Hungry.");
                Debug.Log("I'm Hungry.");
                stateMachine.OnStateEnd();
                break;
            // Play
            case 2:
                stateMachine.isWalking = false;
                stateMachine.wantPlay = true;
                stateMachine.textUI.SetText("Cat: Go play!");
                Debug.Log("Go play!");
                stateMachine.OnStateEnd();
                break;
            default:
                Debug.Log("No one here.");
                MoveToNextTaget();
                break;
        }
    }

    void MoveToNextTaget()
    {
        stateMachine.agent.SetDestination(stateMachine.wayToWalk[current].position);
    }

    public override void OnCollision(Collider other)
    {
        if(other.tag == "Dog")
        {
            stateMachine.isWalking = false;
            stateMachine.seeDog = true;
            stateMachine.textUI.SetText("Cat: SHIT, SHIT, SHIT!!!");
            Debug.Log("SHIT, SHIT, SHIT !!!");
            stateMachine.OnStateEnd();
        }
    }
}
