using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using UnityEngine.AI;

public class WakeUpState : BaseState
{
    int cpt;

    public override void OnStart(StateMachine fsm)
    {
        //UnityEngine.Debug.Log(fsm.name);
        //fsm.transform.position = new Vector3(fsm.cat.transform.position.x, 50, fsm.cat.transform.position.z);
        fsm.GetComponent<CapsuleCollider>().height = 0.01f;
        UnityEngine.Debug.Log(fsm.transform.position);
    }

    public override void OnStateEnd()
    {
        throw new NotImplementedException();
    }

    public override void OnUpdate()
    {
        //throw new NotImplementedException();
    }
}
