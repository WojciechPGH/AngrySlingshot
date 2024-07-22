using UnityEngine;

public class Goal : MonoBehaviour
{
    public static bool goalMet = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Projectile"))
        {
            goalMet = true;
            Material m = GetComponent<Renderer>().material;
            Color c = m.color;
            c.a = 1f;
            m.color = c;
        }
    }
}
