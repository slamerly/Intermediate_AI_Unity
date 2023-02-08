using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LitterState : BaseState
{
    bool ready = false;
    float delay = 2f;

    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        stateMachine.textUI.SetText("Cat: Toilet time.");
        Debug.Log("Toilet time");
        stateMachine.agent.SetDestination(stateMachine.litter.position);
    }
    public override void OnUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.litter.position) <= 1f)
        {
            if (!ready)
            {
                stateMachine.delay = delay;
                ready = true;
            }
            else if (stateMachine.delay <= 0)
            {
                stateMachine.litter.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.Lerp(stateMachine.litter.GetComponent<Renderer>().materials[0].GetColor("_Color"), new Color(0.4f, 0.4f, 0.4f, 1f), Time.deltaTime));
            }
            if(stateMachine.litter.GetComponent<Renderer>().materials[0].GetColor("_Color") == new Color(0.4f, 0.4f, 0.4f, 1f))
                OnStateEnd();
        }
    }

    public override void OnStateEnd()
    {
        stateMachine.textUI.SetText("Cat: I'm tired.");
        Debug.Log("I'm tired");
        stateMachine.needLitter = false;
        stateMachine.isTired = true;
        stateMachine.OnStateEnd();
    }

    public override void OnCollision(Collider other)
    {
    }
}
