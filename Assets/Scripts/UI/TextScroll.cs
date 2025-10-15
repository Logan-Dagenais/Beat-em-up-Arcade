using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private float ScrollSpeed;
    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (ScrollSpeed * Time.deltaTime));
    }
}
