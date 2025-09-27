using UnityEngine;
using UnityEngine.UIElements;

public class FallingObjectScript : MonoBehaviour
{
    [SerializeField] BoxCollider2D hitbox;
    [SerializeField] BoxCollider2D fell;
    [SerializeField] GameObject stagnantParticles;
    [SerializeField] GameObject explosionparticle;
    [SerializeField] GameObject rockfallingparticle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("player under rock");
        gameObject.AddComponent<Rigidbody2D>();
        hitbox.enabled = false;
        fell.enabled = true;
        stagnantParticles.SetActive(false);
        Instantiate(rockfallingparticle, (gameObject.transform.position), Quaternion.identity);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Instantiate(explosionparticle, (gameObject.transform.position), new Quaternion(90f, 90f, -90f, 0f));
    }
}
