using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayState : BaseState
{
    StateMachine stateM;
    bool ready = false;
    float delay = 2f;
    float rotationSpeed = 2;
    float delayMovement = 5f;
    int rand = 0;

    public override void OnStart(StateMachine fsm)
    {
        stateM = fsm;
        Debug.Log("I have something behind me, I need to cathc it.");
        stateM.agent.SetDestination(stateM.play.transform.position);
    }
    public override void OnUpdate()
    {
        if (Vector3.Distance(stateM.transform.position, stateM.play.transform.position) <= 3f)
        {
            stateM.agent.isStopped = true;
            // Wait
            if (!ready)
            {
                Debug.Log(ready);
                ready = true;

                // Switch Camera
                Camera.main.enabled = false;
                stateM.cameraPlay.enabled = true;

                stateM.delay = delay;
            }
            else if (stateM.delay <= 0)
            {
                // Turn arround
                if (delayMovement <= 0)
                {
                    rand = Random.Range(0, 3);
                    Debug.Log(rand);
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
            // Tired
            case 2:
                stateM.wantPlay = false;
                stateM.isTired = true;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().enabled = true;
                stateM.cameraPlay.enabled = false;
                Debug.Log("I'm Tired");
                stateM.OnStateEnd();
                break;
            default:
                break;
        }
    }

    void CatchIt(int randomInt)
    {
        if (randomInt == 0)
        {
            stateM.transform.RotateAround(stateM.play.transform.position, Vector3.up, -1 * 100 * Time.deltaTime);
            stateM.transform.Rotate(new(stateM.transform.rotation.x, stateM.transform.rotation.y + (-1 * rotationSpeed), stateM.transform.rotation.z), Space.World);
        }
        else
        {
            stateM.transform.RotateAround(stateM.play.transform.position, Vector3.up, randomInt * 100 * Time.deltaTime);
            stateM.transform.Rotate(new(stateM.transform.rotation.x, stateM.transform.rotation.y + (randomInt * rotationSpeed), stateM.transform.rotation.z), Space.World);
        }
    }
}
