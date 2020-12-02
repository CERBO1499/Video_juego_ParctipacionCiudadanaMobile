using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}