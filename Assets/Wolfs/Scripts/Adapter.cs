#if (UNITY_STANDALONE)
using UnityEngine;
public static class Adapter
{
    public static Vector3 zero => Vector3.zero;
    public static Vector3 Normalized(Vector3 vec) => vec.normalized;
    public static float MagnitudeSquared(Vector3 vec) => vec.sqrMagnitude;
    public static float Magnitude(Vector3 vec) => vec.magnitude;
    public static float Sqrt(float value) => Mathf.Sqrt(value);
    public static Vector3 MoveTowards(Vector3 fromVec, Vector3 toVec, float delta) => Vector3.MoveTowards(fromVec,toVec,delta);
}
#else
using System.Numerics;
public static class Adapter
{
    public static Vector3 zero => Vector3.Zero;
    public static Vector3 Normalized(Vector3 vec) => Vector3.Normalize(vec);
    public static float MagnitudeSquared(Vector3 vec) => vec.LengthSquared();
    public static float Magnitude(Vector3 vec) => vec.Length();
    public static float Sqrt(float value) => MathF.Sqrt(value);
    public static Vector3 MoveTowards(Vector3 fromVec, Vector3 toVec, float delta)
    {
        if (fromVec == toVec) return fromVec;
        var diff = toVec - fromVec;
        var dist = Magnitude(diff);
        if (dist <= delta) return toVec;
        var dir = Normalized(diff);
        var moveVec = dir*delta;
        return fromVec + moveVec;
    }
}
#endif