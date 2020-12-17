
using System.Collections.Generic;
using UnityEngine;
#region Enum
public enum MemoryBoxTypeAldea
{
    problem,
    solution
}
#endregion
public class ManagerMemoriAldea : MonoBehaviour
{
    int counterCorrect=0;

    public int CounterCorrect { get => counterCorrect; set => counterCorrect = value; }

    public void ValidateMemoriAldea(PieceUnanswerd firstPiece,PieceUnanswerd secondPiece)
    {     

        if (firstPiece.PieceDiscoveredName == secondPiece.PieceDiscoveredName)
        {
            StartCoroutine(firstPiece.ImageCompleteCreace());
            StartCoroutine(secondPiece.ImageCompleteCreace());
            CounterCorrect++;
            print("CountCorrec" + CounterCorrect);
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
