using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform child = transform.GetChild(0);
        ProjectileScript projectileScript = GetComponent<ProjectileScript>();

        if (child.TryGetComponent(out ProjectileScript projScript))
        {
            projScript.Direction.x = -GetComponent<ProjectileScript>().Direction.x;
        }

        Destroy(child.gameObject, projectileScript.lifeTime);
    }

}
