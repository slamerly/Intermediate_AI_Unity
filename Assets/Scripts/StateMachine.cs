using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public Transform food;
    public Transform play;
    public Transform litter;
    public Transform bed;
    public Camera cameraPlay;
    public List<Transform> wayToWalk;

    public TextMeshProUGUI textUI;

    public bool isHungry, isWalking, seeDog, isTired, wantPlay, needLitter, sleepOver;
    public float delay = 0;
    public Color defaultColorLitter;

    public NavMeshAgent agent { get; private set; }

    BaseState currentState;
    private void Awake()
    {
        currentState = new WakeUpState();
        agent = GetComponent<NavMeshAgent>();
        defaultColorLitter = litter.GetComponent<Renderer>().materials[0].GetColor("_Color");
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
        if (needLitter)
            currentState = new LitterState();
        if (seeDog)
            currentState = new RunAwayState();
        if (isTired)
            currentState = new SleepState();
        if (sleepOver)
            currentState = new WakeUpState();

        currentState.OnStart(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        currentState.OnCollision(other);
    }
}
