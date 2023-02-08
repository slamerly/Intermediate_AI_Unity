using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public Transform food;
    public GameObject play;
    public Camera cameraPlay;
    public List<Transform> wayToWalk;

    public bool isHungry, isWalking, seeDog, isTired, wantPlay;
    public float delay = 0;

    public NavMeshAgent agent { get; private set; }

    BaseState currentState;
    private void Awake()
    {
        currentState = new WakeUpState();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        currentState.OnStart(this);
    }

    void Update()
    {
        if (delay <= 0)
        {
            currentState.OnUpdate();
        }
        delay -= Time.deltaTime;
    }

    public void OnStateEnd()
    {
        if (isHungry)
            currentState = new EatState();
        if (isWalking)
            currentState = new WalkState();
        if (wantPlay)
            currentState = new PlayState();

        currentState.OnStart(this);
    }
}
