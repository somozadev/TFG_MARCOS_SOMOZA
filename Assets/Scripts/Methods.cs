using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;

public static class ThreadSafeRandom
{
    [ThreadStatic] private static System.Random Local;

    public static System.Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}


//https://stackoverflow.com/questions/273313/randomize-a-listt
static class MyExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}


public static class Methods
{


    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
            result += 360f;
        return result;
    }

    public static IEnumerator EnableAnim(this GameObject obj)
    {
        yield return new WaitForSeconds(0.2f);
        float elapsedTime = 0;
        float waitTime = 1f;
        obj.transform.localScale = Vector3.zero;
        obj.SetActive(true);
        while (elapsedTime < waitTime)
        {
            obj.transform.localScale = Vector3.Lerp(obj.transform.localScale, Vector3.one, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        obj.transform.localScale = Vector3.one;
        yield return null;

    }

    public static Shop[] LoadShops()
    {
        Shop[] shops = Resources.LoadAll<Shop>("Shops/");
        return shops;
    }

    public static T PickOne<T>(this IEnumerable<T> col)
    {
        int rnd = UnityEngine.Random.Range(0, col.Count());
        return col.ToArray()[rnd];
    }
    public static T PickOneNoRepeat<T>(this IEnumerable<T> col, T lastPick)
    {
        int rnd = UnityEngine.Random.Range(0, col.Count());
        var pick = col.ToArray()[rnd];
        while (pick.Equals(lastPick))
        {
            rnd = UnityEngine.Random.Range(0, col.Count());
            pick = col.ToArray()[rnd];
        }
        return pick;
    }


}

