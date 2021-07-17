using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLine : MonoBehaviour
{
    GameObject init;
    List<GameObject> bullets;
    bool updt;
    [SerializeField] int index;
    private void Awake()
    {
        //     GetComponent<LineRenderer>().SetPosition(0, init.transform.position);
        //     GetComponent<LineRenderer>().SetPosition(1, init.transform.position);

    }
    private void FixedUpdate()
    {
        if (updt)
        {
            Perf();
        }
    }
    private void Perf()
    {
        if (bullets.Count <= 1)
            return;

        LineRenderer newLine = GetComponent<LineRenderer>();
        if (init == null)
        {
            StartCoroutine(WaitToDestroyLineRenderer(newLine, 0f));
            return;
        }





        index = bullets.IndexOf(init);

        if (index > 0)
        {
            if (init != null)
            {
                Vector3 pos2 = bullets[bullets.IndexOf(init)-1].transform.position;
                newLine.SetPosition(0, init.transform.position);
                newLine.SetPosition(1, pos2);

            }

        }
        transform.parent = init.transform;
        StartCoroutine(WaitToDestroyLineRenderer(newLine, 8f));

    }
    public void DoUpdateLine(GameObject init, List<GameObject> bullets)
    {
        this.init = init;
        this.bullets = bullets;
        updt = true;
    }

    private IEnumerator WaitToDestroyLineRenderer(LineRenderer line, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(line.gameObject);
    }
}
