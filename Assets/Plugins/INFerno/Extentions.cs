using System.Collections.Generic;
using UnityEngine;


public static class Extentions
{
    public static T Random<T>(this T[] array)
    {
        if (array == null) Debug.LogError("Can't random: array is null");

        return array[ UnityEngine.Random.Range(0, array.Length) ];
    }

    public static T Random<T>(this List<T> list)
    {
        if (list == null) Debug.LogError("Can't random: list is null");

        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static Vector3 Dimention(this Vector3 vector, bool x, bool y, bool z)
    {
        if (x == false) vector.x = 0;
        if (y == false) vector.y = 0;
        if (z == false) vector.z = 0;

        return vector;
    }

    public static Vector3 ToHorizontal(this Vector3 vector) => Dimention(vector, true, false, true);

    public static Vector3 ToVertical(this Vector3 vector) => Dimention(vector, false, true, false);
}
