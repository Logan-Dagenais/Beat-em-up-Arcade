using UnityEngine;

public class SoundManager1 : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip clip;
    public bool alwaysOn;
    public Animator animator;
    private AudioSource audioSource;
    public string walkStateName = "Walk"; 
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(walkStateName))
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
    }
}