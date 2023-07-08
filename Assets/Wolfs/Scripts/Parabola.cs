using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    public float a;
    public float b;
    public float c;

    private void Update()
    {
        Draw(1000);
    }
    private void Draw(int iterations)
    {
        var xs = Points(-10,10,iterations);
        var ys = new float[xs.Length];
        for (int i = 0; i < xs.Length; i++)
            ys[i] = Evaluate(xs[i]);
        var points = new Vector3[xs.Length];
        for (int i = 0; i < xs.Length; i++)
        {
            points[i] = new Vector3(xs[i],ys[i],0);
        }
        Draw(points);
    }
    private void Draw(Vector3[] points)
    {
        var length = points.Length;
        if (length < 2) return;
        Vector3 p0 = points[0];
        Vector3 p1;
        for (int i = 1; i < length; i++)
        {
            p1 = points[i];
            Debug.DrawLine(p0,p1,Color.green);
            p0 = p1;
        }
    }
    private float[] Points(float p0, float p1, int iterations)
    {
        var pointsCount = iterations+1;
        var array = new float[pointsCount];
        var min = Mathf.Min(p0,p1);
        var max = Mathf.Max(p0,p1);
        for (int i = 0; i < pointsCount; i++)
        {
            var t = (float)i/iterations;
            array[i] = Mathf.Lerp(min,max,t);
        }
        return array;
    }
    private float Evaluate(float x)
    {
        return a*x*x + b*x + c;
    }
}
