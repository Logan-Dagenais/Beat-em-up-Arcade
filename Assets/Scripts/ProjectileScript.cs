using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    [SerializeField] public AttackProperties atk;

    [HideInInspector] public Vector2 Direction;
    [SerializeField] private float BulletSpeed;

    [SerializeField] private bool pierce;
    public List<GameObject> HitList;

    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (BulletSpeed != 0)
        {
            rb2d.MovePosition((Vector2)transform.position + Direction * BulletSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 ||
           collision.gameObject.CompareTag(gameObject.tag) ||
           HitList.Contains(collision.gameObject))
        {
            return;
        }

        CharacterScript character = null;
        collision.transform.parent?.TryGetComponent<CharacterScript>(out character);

        if (character == null)
        {
            return;
        }

        bool hitfromLeft = Direction.x != 0 ? Direction.x > 0 :
                           transform.position.x < collision.transform.position.x;

        atk.Low = transform.position.y < collision.transform.position.y;

        character.HitReaction(atk, hitfromLeft);

        if (!pierce)
        {
            Destroy(gameObject);
        }

        HitList.Add(collision.gameObject);
    }
}
