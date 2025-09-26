using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Warp : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("level1beat");
            SceneManager.LoadScene("Level2Prototype");
        }
    }
}
