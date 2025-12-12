using UnityEngine;

public class LightningAnim : MonoBehaviour
{
    Animator anim;
    Animator childAnim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        childAnim = transform.GetChild(0).GetComponent<Animator>();
    }

    public void PlayEffect()
    {
        anim.Play("lightning");
        childAnim.Play("lightning 2");
    }
}
