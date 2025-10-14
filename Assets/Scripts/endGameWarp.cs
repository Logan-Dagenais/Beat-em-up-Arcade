using UnityEngine;

public class endGameWarp : MonoBehaviour
{
    [SerializeField] private GameObject winUI;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winUI.SetActive(true);
        }
    }
}
