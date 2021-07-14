using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLine : MonoBehaviour
{
    GameObject init;
    List<GameObject> bullets;
    bool updt;
    private void FixedUpdate()
    {
        if (updt)
        {
            Perf();
        }
    }
    private void Perf()
    {
        LineRenderer newLine = GetComponent<LineRenderer>();
        if (init == null)
        {
            StartCoroutine(WaitToDestroyLineRenderer(newLine, 0.2f));
            return;
        }
        newLine.SetPosition(0, init.transform.position);


        if (bullets.IndexOf(init) - 1 >= 0)
        {
            if (init != null)
            {
                Vector3 pos2 = bullets[bullets.IndexOf(init) - 1].transform.position;
                newLine.SetPosition(1, pos2);
            }

        }
        StartCoroutine(WaitToDestroyLineRenderer(newLine, 2f));

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
