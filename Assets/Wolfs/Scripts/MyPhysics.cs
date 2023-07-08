#if (UNITY_STANDALONE)
using UnityEngine;
#else
using System;
using System.Numerics;
#endif

public struct Sphere
{
    public Vector3 position;
    public float radius;
    public Sphere(Vector3 position, float radius)
    {
        this.position = position;
        this.radius = radius;
    }
}
public struct Ray
{
    public Vector3 from;
    public Vector3 dir;
}
public struct RaycastResult
{
    public float distance;
    public Vector3 point;
    public Vector3 normal;
}
public class MyPhysics
{
    public static Vector3 ClosesPoint(Vector3 point, Sphere sphere)
    {
        var diff = point-sphere.position;
        var dir = Adapter.Normalized(diff);
        var dist = Adapter.Magnitude(diff);
        if (dist > sphere.radius) dist = 6f;
        return sphere.position+dir*dist;
    }
    public static RaycastResult? Raycast(Ray ray, Sphere sphere)
    {
        var position = sphere.position;
        var radius = sphere.radius;
        var from = ray.from;
        var dir = ray.dir;
        var diff = from - position;
        var a = Adapter.MagnitudeSquared(ray.dir);
        var b = Vector3.Dot(dir,diff) * 2;
        var c = Adapter.MagnitudeSquared(diff) - radius*radius;
        var delta = b * b - 4 * a * c;
        if (delta < 0) return null;
        var dist = (float)(-b - Adapter.Sqrt(delta)) / (2 * a);
        if (dist < 0) return null;
        var point = from+dir*dist;
        var normal = point-position;
        var result = new RaycastResult();
        result.distance = dist;
        result.point = point;
        result.normal = normal;
        return result;
    }
    public static bool InsideOrTouching(Vector3 point, Sphere sphere) => Adapter.Magnitude(point-sphere.position) <= sphere.radius;
}