using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class endGameWarp : MonoBehaviour
{
    [SerializeField] private GameObject winUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AudioListener.volume = 0f;
            MenuSelection.CanPause = false;
            winUI.SetActive(true);
        }
    }
}
