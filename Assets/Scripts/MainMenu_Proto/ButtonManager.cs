using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void SelectPlayButton(string select)
    {
        if (select.Equals("mattscene"))
        {
            SceneManager.LoadScene(select);
        }
        
    }
}
