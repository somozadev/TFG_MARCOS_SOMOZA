// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Corridor : MonoBehaviour
// {
//     public Transform[] Exits;
//     public Collider bounds;
//     private Level level;
    
    
//     public void Initialize(Level lvl)
//     {
//         level = lvl;
//         transform.SetParent(level.transform);
//         level.RegisterNewSection(bounds);
//         if(level.SizeAvailable > 0)
//             GenerateHoles();
//     }

   
//     void GenerateHoles()
//     {
//         foreach(Transform exit in Exits)
//         {
//             GenerateHall(exit);
//         }
//     }

//    void GenerateHall(Transform exit)
//     {
//         Hall hall = Instantiate(level.halls.PickOne(), exit).GetComponent<Hall>();
//         if(level.IsSectionValid(hall.bounds, bounds))
//         {
//             hall.Initialize(level);
//         }
//         else
//         {
//             Destroy(hall.gameObject);
//         }
//     }
// }
