using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    public PatrolPoint nextPoint;
    public Vector3 Point { get { return transform.position; } }

    private void Draw()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Point, 0.2f);
        Gizmos.color = Color.yellow;
        if (nextPoint != null)
            Gizmos.DrawLine(Point, nextPoint.Point);

    }
    private void OnDrawGizmos()
    {
        Draw();
    }
}
