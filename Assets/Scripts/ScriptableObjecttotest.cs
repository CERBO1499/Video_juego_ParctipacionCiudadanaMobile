using UnityEngine;

[CreateAssetMenu(fileName = "test", menuName ="test", order = 1)]
public class ScriptableObjecttotest : ScriptableObject
{
    private void Awake()
    {
        Debug.Log("aWAKE");
    }
    private void OnDestroy()
    {
        Debug.Log("DESTROYU");
    }
}