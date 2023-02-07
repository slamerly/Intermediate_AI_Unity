using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkState : BaseState
{
    StateMachine stateM;
    int current = 0;
    bool ready = false;
    float delay = 2f;

    public override void OnStart(StateMachine fsm)
    {
        stateM = fsm;
        Debug.Log("Go walk around the house!");
        MoveToNextTaget();
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(stateM.transform.position, stateM.wayToWalk[current].position) <= 1f)
        {
            if(!ready)
            {
                ready = true;
                stateM.delay = delay;
            }
            else if (stateM.delay <= 0)
            {
                current = (current + 1) % stateM.wayToWalk.Count;
                ready = false;
                //Debug.Log("No one here");
                OnStateEnd();
                MoveToNextTaget();
            }
        }
    }

    public override void OnStateEnd()
    {
        int rand = Random.Range(0, 2);

        switch(rand)
        {
            // Continue
            case 0:
                Debug.Log("No one here.");
                break;
            // Hungry
            case 1:
                stateM.isHungry = true;
                Debug.Log("Hungry: " + stateM.isHungry);
                stateM.OnStateEnd();
                break;
            // See dog
            case 2:
                stateM.seeDog = true;
                Debug.Log("SHIT, SHIT, SHIT !!!");
                stateM.OnStateEnd();
                break;
            // Play
            case 3:
                stateM.wantPlay = true;
                Debug.Log("I have something behind me, I need to cathc it.");
                stateM.OnStateEnd();
                break;
            default:
                Debug.Log("No one here.");
                break;
        }
    }

    void MoveToNextTaget()
    {
        stateM.agent.SetDestination(stateM.wayToWalk[current].position);
    }


}
