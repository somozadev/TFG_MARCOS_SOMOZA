using UnityEngine;
using System.Collections.Generic;
public class Bounds : MonoBehaviour
{
        public IEnumerable<Collider> Colliders => GetComponentsInChildren<Collider>();
}


