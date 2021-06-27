using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpController : MonoBehaviour
{

    [SerializeField] SceneItem xpItem;
    [SerializeField] Item[] xps;

    [SerializeField] int[] sol;
    [SerializeField] int[] cost;




    public void PerformXps(int C, Transform pos)
    {
        Debug.Log("COST:" + C);
        sol = new int[xps.Length];
        cost = new int[xps.Length];

        for (int i = 0; i < xps.Length; i++)
            cost[i] = xps[i].Cuantity;

        for (int i = 0; i < cost.Length; i++)
        {
            while (cost[i] <= C)
            {
                C -= cost[i];
                sol[i] += 1;
            }
        }

        PerformInstantiations(pos);

    }

    public void PerformInstantiations(Transform pos)
    {
        for (int i = 0; i < sol.Length; i++)
        {
            while (sol[i] > 0)
            {
                xpItem.item = xps[i];
                GameObject xp = Instantiate(xpItem.gameObject, (pos.position + Vector3.up) + new Vector3(Random.Range(-2f,2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)), Quaternion.identity, transform);
                sol[i] -= 1;
                xp.GetComponent<Rigidbody>().AddForce(Vector3.up);
            }
        }
    }



}
