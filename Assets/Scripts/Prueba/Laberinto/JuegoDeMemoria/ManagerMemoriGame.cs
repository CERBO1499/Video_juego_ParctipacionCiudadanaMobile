using System.Collections.Generic;
using UnityEngine;

#region Enum
public enum MemoryBoxType
{
    problem,
    solution
}
#endregion

public class ManagerMemoriGame : MonoBehaviour
{
    [SerializeField]
    MemoriBox[] mBoxProblems;
    Dictionary<string, MemoriBox> diccionaryManager;

    int CounterCorrect=0;

    public int CounterCorrect1 { get => CounterCorrect; set => CounterCorrect = value; }

    private void Awake()
    {
        diccionaryManager = new Dictionary<string, MemoriBox>();

        diccionaryManager.Add(mBoxProblems[0].problema.name, mBoxProblems[0]);
        diccionaryManager.Add(mBoxProblems[1].problema.name, mBoxProblems[1]);
        diccionaryManager.Add(mBoxProblems[2].problema.name, mBoxProblems[2]);
    }

    public void Validate(BehaviourPieceEmpty firstPiece, BehaviourPieceEmpty secondPiece)
    {
        if (firstPiece.Ptype != secondPiece.Ptype)
        {
            MemoriBox memoryBox = (firstPiece.Ptype == MemoryBoxType.problem) ? diccionaryManager[firstPiece.PieceDiscoveredName] : diccionaryManager[secondPiece.PieceDiscoveredName];

            if (firstPiece.Ptype == MemoryBoxType.solution)
            {
                bool founded = false;

                for (int i = 0; i < memoryBox.soluciones.Length; i++)
                {
                    if (firstPiece.PieceDiscoveredName == memoryBox.soluciones[i].name)
                    {
                        founded = true;

                        StartCoroutine(firstPiece.ImageCompleteCreace());

                        CounterCorrect1++;         

                        StartCoroutine(secondPiece.ReturnRotate());

                        StartCoroutine(secondPiece.ApearImageBase());

                        break;
                    }
                }

                if (!founded)
                {
                    StartCoroutine(firstPiece.ReturnRotate());

                    StartCoroutine(firstPiece.ApearImageBase());

                    StartCoroutine(secondPiece.ReturnRotate());

                    StartCoroutine(secondPiece.ApearImageBase());
                }
            }
            else
            {
                bool founded = false;

                for (int i = 0; i < memoryBox.soluciones.Length; i++)
                {
                    if (secondPiece.PieceDiscoveredName == memoryBox.soluciones[i].name)
                    {
                        founded = true;

                        StartCoroutine(secondPiece.ImageCompleteCreace());

                        CounterCorrect1++;

                        StartCoroutine(firstPiece.ReturnRotate());

                        StartCoroutine(firstPiece.ApearImageBase());

                        break;
                    }
                }

                if (!founded)
                {
                    StartCoroutine(firstPiece.ReturnRotate());

                    StartCoroutine(firstPiece.ApearImageBase());

                    StartCoroutine(secondPiece.ReturnRotate());

                    StartCoroutine(secondPiece.ApearImageBase());
                }
            }
        }
        else
        {
            StartCoroutine(firstPiece.ReturnRotate());

            StartCoroutine(firstPiece.ApearImageBase());

            StartCoroutine(secondPiece.ReturnRotate());

            StartCoroutine(secondPiece.ApearImageBase());
        }
    }



}
