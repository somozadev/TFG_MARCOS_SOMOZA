using UnityEngine;

public class FloorBulletIgnorer : MonoBehaviour
{
    [SerializeField] GameObject[] bulletPrefabs;
    [SerializeField] GameObject colL;
    [SerializeField] GameObject colR;


    // private void Awake()
    // {
    //     SetupCollisions();
    // }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 11)
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.gameObject.GetComponent<Collider>(), true);
        }
    }

    void SetupCollisions()
    {
        Physics.IgnoreLayerCollision(11, 12, true);
        // foreach (GameObject bullet in bulletPrefabs)
        // {
        //     Physics.IgnoreCollision(colL.GetComponent<Collider>(), bullet.GetComponent<Collider>(), true);
        //     Physics.IgnoreCollision(colR.GetComponent<Collider>(), bullet.GetComponent<Collider>(), true);
        // }
    }
}
