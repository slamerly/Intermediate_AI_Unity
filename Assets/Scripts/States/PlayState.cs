using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayState : BaseState
{
    bool ready = false;
    float delay = 2f;
    float rotationSpeed = 2;
    float delayMovement = 2.5f;
    int nbPlay = 0;
    int nbPlayMax = 4;
    int rand = 0;

    public override void OnStart(StateMachine fsm)
    {
        stateMachine = fsm;
        stateMachine.textUI.SetText("Cat: I have something behind me, I need to catch it.");
        Debug.Log("I have something behind me, I need to catch it.");
        stateMachine.agent.SetDestination(stateMachine.play.position);
    }
    public override void OnUpdate()
    {
        if (Vector3.Distance(stateMachine.transform.position, stateMachine.play.position) <= 3f)
        {
            stateMachine.agent.isStopped = true;
            // Wait
            if (!ready)
            {
                ready = true;

                // Switch Camera
                Camera.main.enabled = false;
                stateMachine.cameraPlay.enabled = true;

                stateMachine.delay = delay;
            }
            else if (stateMachine.delay <= 0)
            {
                // Turn arround
                if (delayMovement <= 0)
                {
                    rand = Random.Range(0, 2);
                    nbPlay++;
                    delayMovement = 5f;
                }
                else
                    OnStateEnd();
                delayMovement -= Time.deltaTime;
            }
        }
    }

    public override void OnStateEnd()
    {
        if (nbPlay <= nbPlayMax)
        {
            switch (rand)
            {
                // Continue -1
                case 0:
                    CatchIt(rand);
                    break;
                // Continue 1
                case 1:
                    CatchIt(rand);
                    break;
                default:
                    break;
            }
        }
        else
        {
            stateMachine.agent.isStopped = false;
            stateMachine.wantPlay = false;
            stateMachine.isTired = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
            stateMachine.cameraPlay.enabled = false;
            stateMachine.textUI.SetText("Cat: I'm tired.");
            Debug.Log("I'm Tired");
            stateMachine.OnStateEnd();
        }
    }

    void CatchIt(int randomInt)
    {
        if (randomInt == 0)
        {
            stateMachine.transform.RotateAround(stateMachine.play.position, Vector3.up, -1 * 100 * Time.deltaTime);
            stateMachine.transform.Rotate(new(stateMachine.transform.rotation.x, stateMachine.transform.rotation.y + (-1 * rotationSpeed), stateMachine.transform.rotation.z), Space.World);
        }
        else
        {
            stateMachine.transform.RotateAround(stateMachine.play.position, Vector3.up, randomInt * 100 * Time.deltaTime);
            stateMachine.transform.Rotate(new(stateMachine.transform.rotation.x, stateMachine.transform.rotation.y + (randomInt * rotationSpeed), stateMachine.transform.rotation.z), Space.World);
        }
    }

    public override void OnCollision(Collider other)
    {
    }
}
