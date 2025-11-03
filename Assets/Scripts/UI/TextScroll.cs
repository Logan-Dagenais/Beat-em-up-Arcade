using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private float ScrollSpeed;
    [SerializeField] private float EndGoal;
    [SerializeField] private SkipScroll Skip;
    private void Update()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y + (ScrollSpeed * Time.deltaTime));
        if(gameObject.transform.position.y >= EndGoal)
        {
            Skip.MoveOn();
        }
    }
}
