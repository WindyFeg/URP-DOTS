using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
public class Fade : State
{
    public float Duration { get; set; } = 2.0f;
    private float deltaTime = 0.0f;
    private FadeType m_fadeType;
    private SpriteRenderer m_spriteRenderer;
    private SplashScreen m_splash;
    public enum FadeType
    {
        FADE_IN,
        FADE_OUT,
    }
    public Fade(FSM fsm, SplashScreen splash, FadeType fadeType = FadeType.FADE_IN) : base(fsm)
    {
        m_splash = splash;
        m_spriteRenderer = splash.spriteLogo.GetComponent<SpriteRenderer>();
        m_fadeType = fadeType;
    }
    public override void Enter()
    {
        deltaTime = Time.deltaTime;
        base.Enter();
        switch (m_fadeType)
        {
            case FadeType.FADE_IN:
                Debug.Log("Entering: FadeIn State");
                break;
            case FadeType.FADE_OUT:
                Debug.Log("Entering: FadeOut State");
                break;
        }
    }
    public override void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime > Duration)
        {
            switch (m_fadeType)
            {
                case FadeType.FADE_IN:
                    int nextid = (int)SplashScreen.SplashStates.PLAY_AUDIO;
                    State nextState = m_fsm.GetState(nextid);
                    m_fsm.SetCurrentState(nextState);
                    break;
                case FadeType.FADE_OUT:
                    m_splash.Exit();
                    break;
            }
        }
        if (m_spriteRenderer != null)
        {
            switch (m_fadeType)
            {
                case FadeType.FADE_IN:
                    m_spriteRenderer.material.color =
                        new Color(1.0f, 1.0f, 1.0f, deltaTime / Duration);
                    break;
                case FadeType.FADE_OUT:
                    m_spriteRenderer.material.color =
                        new Color(1.0f, 1.0f, 1.0f, 1.0f - deltaTime / Duration);
                    break;
            }
        }
    }
}