using UnityEngine;
using Patterns;
public class SplashScreen : MonoBehaviour
{
    public GameObject spriteLogo;
    public AudioClip audioLogo;
    /* 
     * We create enums to represent the 
     * unique ids of the different states.
     */
    public enum SplashStates
    {
        FADE_IN = 0,
        PLAY_AUDIO,
        FADE_OUT,
    }
    private FSM m_fsm = new FSM();
    // Start is called before the first frame update
    void Start()
    {
        /*
         * Create the three states and add
         * to the fsm.
         */
        m_fsm.Add((int)SplashStates.FADE_IN, new Fade(m_fsm, this));
        m_fsm.Add((int)SplashStates.PLAY_AUDIO, new PlayAudio(m_fsm, this));
        m_fsm.Add((int)SplashStates.FADE_OUT, new Fade(m_fsm, this, Fade.FadeType.FADE_OUT));

        /* Set the current state (initial state)
         * of the FSM.
         */
        m_fsm.SetCurrentState(m_fsm.GetState((int)SplashStates.FADE_IN));
    }
    // Update is called once per frame
    void Update()
    {
        /*
         * We call the FSM update here
         */
        if (m_fsm != null)
        {
            m_fsm.Update();
        }
    }

    public void Exit()
    {
        Debug.Log("Splash screen with FSM has exited.");
        m_fsm = null;
    }


}
