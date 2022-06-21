using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Better explosion physics, Ignore object behind obstacle
// Only work with obstacle without rigidbody attached

public class SpawnAndExplode : MonoBehaviour
{
   public GameObject spawn;
   public float radius = 10f;
   public float power = 500f;
   public int spawnCount = 50;
   bool canSpawn = true;

   Collider[] colliders;

   private void Update()
   {
      //DrawLine();
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (canSpawn)
         {
            StartCoroutine(Spawn());
            canSpawn = false;
         }
      }

      //SingleExplosion(transform.position,power,radius,3f);
      MultipleExplosion(transform.position,power,radius,.2f);
   }
   
   // explosion only effect the nearest object ( first object return from raycast )
   // explosion effect target with rigidbody and collision
   // explosion ignore target behind object without rigidbody or collision

   void SingleExplosion(Vector3 center, float force, float rad, float upward)
   {
      if (Input.GetKeyDown(KeyCode.E))
      {
         colliders = Physics.OverlapSphere(center, radius); // take all collider object within rad
         for (int i = 0; i < colliders.Length; i++)
         {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>(); // get rb from each collider
            if (rb != null)
            {
               RaycastHit targetInfo;
               // cast a ray from center to current target collider
               if (Physics.Raycast(transform.position,
               (colliders[i].gameObject.transform.position - transform.position).normalized,
               out targetInfo))
               {
                  // if the ray destination object is current collider object
                  if (targetInfo.collider.gameObject == colliders[i].gameObject)
                  {
                     rb.AddExplosionForce(force, center, rad, upward);
                     print("explode");
                  }
               }
            }
         }
      }
   }

   //
   // explosion effect all target inside range
   // explosion ignore target behind object without rigidbody or collision

   void MultipleExplosion(Vector3 center, float force, float rad, float upward)
   {
      if (Input.GetKeyDown(KeyCode.E))
      {
         colliders = Physics.OverlapSphere(center, radius); // take all collider object within rad
         //Time.timeScale = 0.1f;
         for (int i = 0; i < colliders.Length; i++)
         {
            Rigidbody rb = colliders[i].GetComponent<Rigidbody>(); // get rb from each collider
            if (rb != null)
            {
               // cast a ray from center to current target collider
               RaycastHit[] targetInfo = Physics.RaycastAll(transform.position,
                  (colliders[i].gameObject.transform.position - transform.position).normalized);

               // multiple rangeToIgnore possible =======================================================
               List<float> rangetoIgnore = new List<float>();
               for (int j = 0; j < targetInfo.Length; j++) // ray cast on each collider object 
               {
                  if (!targetInfo[j].collider.gameObject.TryGetComponent(out Rigidbody rb2)) // find object without rigidbody
                  {
                     rangetoIgnore.Add(targetInfo[j].distance); // add distance to list
                  }
               }
               //===========================================================================================
               rangetoIgnore.Sort(); // sort and set the nearest value 
               float range;
               if (rangetoIgnore.Count == 0) // if none then ignore
                  range = 0;
               else
                  range = rangetoIgnore[0];
               //===========================================================================================
               for (int j = 0; j < targetInfo.Length; j++)
               {
                  if (targetInfo[j].distance < range - .1f || range == 0f) // check if target is closer to the center than obstacle
                  {
                     if (targetInfo[j].collider.gameObject.TryGetComponent(out Rigidbody target))
                        target.AddExplosionForce(force, center, rad, upward);
                  }
               }
            }
         }
      }
   }
   void DrawLine()
   {
      if (colliders != null)
      {
         for (int i = 0; i < colliders.Length; i++)
         {
            RaycastHit info;
            if (Physics.Raycast(transform.position,
               (colliders[i].gameObject.transform.position - transform.position).normalized,
               out info))
            {
               if (info.collider.gameObject.TryGetComponent(out Rigidbody rb))
               {
                  if (info.collider.gameObject == colliders[i].gameObject)
                     Debug.DrawLine(transform.position, colliders[i].transform.position, Color.red);
               }
            }
         }
      }
   }
   IEnumerator Spawn()
   {
      for (int i = 0; i < spawnCount; i++)
      {
         Instantiate(spawn, transform.position + new Vector3(0f, 5f, 0f), Quaternion.identity);
         yield return new WaitForSeconds(.05f);
      }
      canSpawn = true;
   }
}
