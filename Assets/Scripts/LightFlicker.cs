using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Animator animator;
    public int flickerChance;

    private void OnEnable()
    {
        if ((Random.value * 100) >= flickerChance)
        {
            animator.SetTrigger("lightFlickers");
        }
    }
}