using UnityEngine;
using System.Collections;
using UnityEngine.Events;
[RequireComponent(typeof(UnityEngine.AudioSource))]

public abstract class State : MonoBehaviour {
    [HideInInspector]
    public UnityEvent spaceListener;
    [HideInInspector]
    public UnityEvent rListener;
    [HideInInspector]
    public UnityEvent ideaListener;
    [HideInInspector]
    public UnityEvent memoryListenerIn;
    [HideInInspector]
    public UnityEvent memoryListenerOut;
    [HideInInspector]

    private AudioSource m_AudioSource;
    private AudioContainer soundContainer;

    public UnityEvent SetSpaceEvent
    {
        set { spaceListener.AddListener(OnSpacePress); }
    }

    public UnityEvent SetReturnEvent
    {
        set { rListener.AddListener(OnReturnPress); }
    }

    public UnityEvent SetIdeaEvent
    {
        set { ideaListener.AddListener(OnIdeaPress); }
    }

    public UnityEvent SetMemoryEventIn
    {
        set { memoryListenerIn.AddListener(OnMemoryPressIn); }
    }

    public UnityEvent SetMemoryEventOut
    {
        set { memoryListenerOut.AddListener(OnMemoryPressOut); }
    }

    public AudioSource SetAudioSource
    {
        set { m_AudioSource = value; }
    }

    public AudioContainer SetAudioContainer
    {
        set { soundContainer = value; }
    }

    public abstract void StateUpdate();

    
    


    void Start()
    {        
        soundContainer = GameManager.Self.GetComponent<AudioContainer>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void OnSpacePress()
    {
        // Quando schiacci spazio fai cose
        m_AudioSource.clip = soundContainer.ControlBodyONSound;
		m_AudioSource.volume = 0.05f;
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    void OnReturnPress()
    {
        // Quando ritorni nel tuo corpo fai cose
        m_AudioSource.clip = soundContainer.ControlBodyOFFSound;
		m_AudioSource.volume = 0.05f;
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    void OnIdeaPress()
    {
        // Quando imprimi un'idea fai cose
        m_AudioSource.clip = soundContainer.ideaControl;
        m_AudioSource.volume = 0.05f;
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }
    
    void OnMemoryPressIn()
    {
        // Quando guardi un ricordo fai cose
        m_AudioSource.clip = soundContainer.memoryControl;
        m_AudioSource.volume = 0.05f;
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
    }

    void OnMemoryPressOut()
    {
        // Quando smetti di guardare un ricordo fai cose
    }
}
