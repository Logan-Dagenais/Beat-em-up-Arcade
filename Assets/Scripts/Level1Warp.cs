using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Warp : MonoBehaviour
{
    public string WhichLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("level1beat");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
