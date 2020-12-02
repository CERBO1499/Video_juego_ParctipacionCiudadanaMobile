using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}