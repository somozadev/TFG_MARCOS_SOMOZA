using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exits : MonoBehaviour
{
    public IEnumerable<Transform> exitSpots => GetComponentsInChildren<Transform>().Where(t => t != transform);
}
