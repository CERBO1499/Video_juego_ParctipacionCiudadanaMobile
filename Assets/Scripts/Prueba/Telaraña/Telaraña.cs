using UnityEngine;

public class Telaraña : MonoBehaviour
{
    #region Information
    [Header("Information")]
    [SerializeField]
    GameObject[] jaikas;
    #endregion

    public void Pass()
    {
        int jaika = 0;

        for (int i = 0; i < jaikas.Length; i++)
        {
            if (jaikas[i].activeSelf)
            {
                jaika = i;

                break;
            }
        }

        jaikas[jaika].SetActive(false);

        jaika++;

        jaikas[jaika].SetActive(true);
    }
}