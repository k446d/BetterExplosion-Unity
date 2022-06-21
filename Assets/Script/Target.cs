using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
   NavMeshObstacle obstacle;
   private Rigidbody rb;
   public float time = 2f;

   private void Start()
   {
      rb = GetComponent<Rigidbody>();
      obstacle = GetComponent<NavMeshObstacle>();
      obstacle.carving = true;
      obstacle.carveOnlyStationary = true;
      obstacle.carvingTimeToStationary = 0.2f;


   }
   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Alpha1))
         obstacle.carving = false;
   }
}
