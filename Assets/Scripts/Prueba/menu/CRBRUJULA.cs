using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRBRUJULA : MonoBehaviour
{
   [SerializeField] Vector3 NorthDirection;
   [SerializeField] Transform Player;
   [SerializeField] Quaternion MissionDirection;

   [SerializeField] RectTransform _northLayer;
   [SerializeField] RectTransform _missionLayer;

   [SerializeField] Transform missionPlace;

   private void Update() {
       changeNorthDirection();
       changeMissionDirection();
   }

   void changeNorthDirection(){
       NorthDirection.y=Player.eulerAngles.z;
       _northLayer.localEulerAngles=NorthDirection;
   }
   void changeMissionDirection(){

       Vector3 dir = transform.position-missionPlace.position;
       MissionDirection = Quaternion.LookRotation(dir);
       MissionDirection.y=-MissionDirection.z;
       MissionDirection.x=0;
       MissionDirection.y=0;

       _missionLayer.localRotation=MissionDirection*Quaternion.Euler(NorthDirection);

   }
}
