using UnityEngine;

public class SnowstormParticle : MonoBehaviour
{
    public GameObject Snowstorm1;
    public GameObject Snowstorm2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("enter snow");
            var changer1 = Snowstorm1.GetComponent<ParticleSystem>().emission;
            changer1.rateOverTime = 16;
            var changer2 = Snowstorm2.GetComponent<ParticleSystem>().emission;
            changer2.rateOverTime = 5.2f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("exit snow");
            var changer1 = Snowstorm1.GetComponent<ParticleSystem>().emission;
            changer1.rateOverTime = 0;
            var changer2 = Snowstorm2.GetComponent<ParticleSystem>().emission;
            changer2.rateOverTime = 0;
        }
    }
}
