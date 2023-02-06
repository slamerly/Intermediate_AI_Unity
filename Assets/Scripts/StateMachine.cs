using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public WakeUpState currentState = new WakeUpState();

    public GameObject cat { get; set; }

    bool isHungry, seeDog, isTired, wantPoop;

    private void Awake()
    {
        cat = gameObject;
    }

    void Start()
    {
        currentState.OnStart(this);
    }

    void Update()
    {
        currentState.OnUpdate();
    }

    public void OnStateEnd()
    {
        


    }
}
