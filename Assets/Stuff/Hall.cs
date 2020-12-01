// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Hall : MonoBehaviour
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
//             GenerateCorridor(exit);
//         }
//     }

//     void GenerateCorridor(Transform exit)
//     {
//         Corridor corridor = Instantiate(level.corridors.PickOne(), exit).GetComponent<Corridor>();
//         if(level.IsSectionValid(corridor.bounds, bounds))
//         {
//             corridor.Initialize(level);
//         }
//         else
//         {
//             Destroy(corridor.gameObject);
//         }
        

//     }
// }
