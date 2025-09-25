using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    [SerializeField] private AttackProperties atk;
    [HideInInspector] public Vector2 Direction;
    [SerializeField] private float BulletSpeed;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition((Vector2)transform.position + Direction * BulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerScript player = collision.GetComponentInParent<PlayerScript>();
            bool hitfromLeft = Direction.x > 0;

            atk.Low = transform.position.y < collision.transform.position.y;

            player.HitReaction(atk, hitfromLeft);

            Destroy(gameObject);
        }
    }
}
