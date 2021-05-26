using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public static class Methods
{
    public static float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
            result += 360f;
        return result;
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

