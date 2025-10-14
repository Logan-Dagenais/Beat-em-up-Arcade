using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D platCollider;
    [SerializeField] private BoxCollider2D trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }
}
