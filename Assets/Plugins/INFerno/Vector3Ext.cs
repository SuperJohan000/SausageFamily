using System.ComponentModel;
using UnityEngine;


public static class Vector3Ext
{
    public static Vector3 AbsMin(params Vector3[] vectors)
    {
        if (vectors == null) Debug.LogError("Can't AbsMin: vectors is null");

        Vector3 min = vectors[0];

        foreach(var vector in vectors)
        {
            min.x = Mathf.Abs(min.x) < Mathf.Abs(vector.x) ? min.x : vector.x;
            min.y = Mathf.Abs(min.y) < Mathf.Abs(vector.y) ? min.y : vector.y;
            min.z = Mathf.Abs(min.z) < Mathf.Abs(vector.z) ? min.z : vector.z;
        }

        return min;
    }

    public static Vector3 AbsMax(params Vector3[] vectors)
    {
        if (vectors == null) Debug.LogError("Can't AbsMax: vectors is null");

        Vector3 max = vectors[0];

        foreach (var vector in vectors)
        {
            max.x = Mathf.Abs(max.x) > Mathf.Abs(vector.x) ? max.x : vector.x;
            max.y = Mathf.Abs(max.y) > Mathf.Abs(vector.y) ? max.y : vector.y;
            max.z = Mathf.Abs(max.z) > Mathf.Abs(vector.z) ? max.z : vector.z;
        }

        return max;
    }


    public static Vector3 NormalizeEulerAngles(Vector3 eulerAngles)
    {
        eulerAngles.x = NormalizeEulerAngle(eulerAngles.x);
        eulerAngles.y = NormalizeEulerAngle(eulerAngles.y);
        eulerAngles.z = NormalizeEulerAngle(eulerAngles.z);

        return eulerAngles;
    }


    public static float NormalizeEulerAngle(float angle)
    {
        angle %= 360;

        return angle > 180 ? angle - 360 : angle < -180 ? angle + 360 : angle;
    }


    public static float SmoothClamp(float value, float maxValue, float startSmoothValue, float endSmoothValue)
    {
        var sign = Mathf.Sign(value);

        value = Mathf.Abs(value);
        maxValue = Mathf.Abs(maxValue);
        endSmoothValue = Mathf.Abs(endSmoothValue);
        startSmoothValue = Mathf.Abs(startSmoothValue);

        startSmoothValue = Mathf.Min(maxValue, startSmoothValue);
        endSmoothValue = Mathf.Max(maxValue, endSmoothValue);

        if (value <= startSmoothValue)
        {
            return value * sign;
        }
        else
        {
            var afterValue = value - startSmoothValue;
            var afterEndSmoothValue = endSmoothValue - startSmoothValue;

            var t = Mathf.Sin( Mathf.Clamp01(afterValue / afterEndSmoothValue) * Mathf.PI / 2);

            return Mathf.Lerp(startSmoothValue, maxValue, t ) * sign;
        }
    }
}
