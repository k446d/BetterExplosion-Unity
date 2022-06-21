using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
   NavMeshAgent agent;
   
   //public NavMeshPath path;
   public Transform target;
   Rigidbody rb;
   private void Start()
   {
      rb = GetComponent<Rigidbody>();
      agent = GetComponent<NavMeshAgent>();
      //agent.destination = target.position;
   }
   private void Update()
   {

      FixBug();
      agent.SetDestination(target.position);
      agent.updateRotation = true;
   }
   void FixBug()
   {
      if(rb.velocity.y < -5)
      {
         rb.constraints = RigidbodyConstraints.FreezeAll;
         rb.constraints = RigidbodyConstraints.None;
         //rb.constraints = RigidbodyConstraints.FreezeRotationX;
         //rb.constraints = RigidbodyConstraints.Free
      }
   }
}
