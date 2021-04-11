using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public IEnumerable<Transform> holes => GetComponentsInChildren<Transform>().Where(t => t != transform);

}
