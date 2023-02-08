using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleepState : BaseState
{
    //float timeSleep = 30;
    bool sleeping;
    Vector3 defaultPosition = Vector3.zero;

    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        stateMachine.textUI.SetText("Cat: Sleeping time.");
        Debug.Log("Sleeping time");
    }

    public override void OnUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.bed.position) <= 1f)
        {
            Init();
            if (defaultPosition == Vector3.zero)
                defaultPosition = stateMachine.transform.position;
            stateMachine.agent.enabled = false;
            stateMachine.transform.position = Vector3.Lerp(stateMachine.transform.position, stateMachine.bed.position, Time.deltaTime);

            if (!sleeping && stateMachine.transform.position.y <= 0.3f)
            {
                sleeping = true;
                //stateMachine.delay = Random.Range(10, timeSleep);
                stateMachine.delay = 10;
            }
            if(sleeping && stateMachine.delay <= 0) 
            {
                OnStateEnd();
            }
        }
        else
        {
            stateMachine.agent.SetDestination(stateMachine.bed.position);
        }
    }

    public override void OnStateEnd()
    {
        stateMachine.transform.position = Vector3.Lerp(stateMachine.transform.position, defaultPosition, Time.deltaTime);

        if (stateMachine.transform.position.y >= defaultPosition.y - 0.5f)
        {
            stateMachine.agent.enabled = true;
            stateMachine.isTired = false;
            stateMachine.sleepOver = true;
            stateMachine.OnStateEnd();
        } 
    }

    public override void OnCollision(Collider other)
    {
    }

    void Init()
    {
        stateMachine.food.localScale = Vector3.Lerp(stateMachine.food.localScale, Vector3.one, Time.deltaTime);
        stateMachine.litter.GetComponent<Renderer>().materials[0].SetColor("_Color", Color.Lerp(stateMachine.litter.GetComponent<Renderer>().materials[0].GetColor("_Color"), stateMachine.defaultColorLitter, Time.deltaTime));
    }
}
