using UnityEngine;

public class SnowstormParticle : MonoBehaviour
{
    public GameObject Snowstorm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("enter snow");
            Snowstorm.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("exit snow");
            Snowstorm.SetActive(false);
        }
    }
}
