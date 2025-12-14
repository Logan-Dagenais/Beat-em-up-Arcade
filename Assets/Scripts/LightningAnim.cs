using UnityEngine;

public class LightningAnim : MonoBehaviour
{
    Animator anim;
    Animator childAnim;
    public AudioSource sound1;
    public AudioSource sound2;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        childAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnEnable()
    {
        anim.Play("lightning");
        childAnim.Play("lightning 2");
        if (!sound1.isPlaying)
        {
            sound1.Play();
            sound2.Play();
        }

        
    }
}
