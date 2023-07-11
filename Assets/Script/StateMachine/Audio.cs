using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
public class PlayAudio : State
{
    public float Duration { get; set; } = 1.0f;
    private float deltaTime = 0.0f;
    private SplashScreen m_splash;
    public PlayAudio(FSM fsm, SplashScreen splash) : base(fsm)
    {
        m_splash = splash;
    }
    // lets discuss what generic functions need to be there in a State.
    public override void Enter()
    {
        deltaTime = Time.deltaTime;
        m_splash.GetComponent<AudioSource>().PlayOneShot(m_splash.audioLogo);
        base.Enter();
        Debug.Log("Entering: PlayAudio State");
    }
    public override void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > 1.0f)
        {
            int nextId = (int)SplashScreen.SplashStates.FADE_OUT;
            State nextState = m_fsm.GetState(nextId);
            m_fsm.SetCurrentState(nextState);
        }
    }
}