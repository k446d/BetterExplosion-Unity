using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
   private void Start()
   {
      
   }
   private void Update()
   {
      Move();
   }

   private void Move()
   {
      transform.position += new Vector3(Input.GetAxisRaw("Horizontal"),
         0f,
         Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime * 5f;
   }
}

