using UnityEngine;

public class DeadEnd : MonoBehaviour
{
    public Bounds bounds;
    public void Initialize(Level level)
    {
        transform.SetParent(level.transform);
        level.RegisterNewDeadEnd(bounds.Colliders);
    }
    
}
