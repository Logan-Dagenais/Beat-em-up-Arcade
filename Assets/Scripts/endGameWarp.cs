using UnityEngine;

public class endGameWarp : MonoBehaviour
{
    [SerializeField] private GameObject winUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MenuSelection.CanPause = false;
            winUI.SetActive(true);
        }
    }
}
