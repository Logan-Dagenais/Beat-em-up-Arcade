using Unity.VisualScripting;
using UnityEngine;

public class AquaductRandomPour : MonoBehaviour
{
    public Animator Aquaduct;
    void OnEnable()
    {
        if ((Random.value * 100)>= 85)
        {
            Aquaduct.SetTrigger("RandomPour");
        }
    }
}
