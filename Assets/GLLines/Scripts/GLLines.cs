using System.Collections.Generic;
using UnityEngine;

public static class GLLines
{
    private static List<Line> lines = new List<Line>();
    private static List<Line> linesScreen = new List<Line>();
    private static Material material;
    private static int clearedFrame = 0;

    public static void DrawBounds(Bounds b, Color color)
    {
        var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3(b.min.x, b.min.y, b.max.z);
        var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        DrawLine(p1, p2, color);
        DrawLine(p2, p3, color);
        DrawLine(p3, p4, color);
        DrawLine(p4, p1, color);
        DrawLine(p5, p6, color);
        DrawLine(p6, p7, color);
        DrawLine(p7, p8, color);
        DrawLine(p8, p5, color);
        DrawLine(p1, p5, color);
        DrawLine(p2, p6, color);
        DrawLine(p3, p7, color);
        DrawLine(p4, p8, color);
    }
    public static void DrawCross(Vector3 center, float radius, Color color)
    {
        var p0 = center;
        var p1 = center+radius*Vector3.right;
        var p2 = center+radius*Vector3.left;
        var p3 = center+radius*Vector3.forward;
        var p4 = center+radius*Vector3.back;
        DrawLine(p0,p1,color);
        DrawLine(p0,p2,color);
        DrawLine(p0,p3,color);
        DrawLine(p0,p4,color);
    }
    public static void DrawCircle(Vector3 center, int iterations, float radius, Color color)
    {
        var points = CirclePoints(center,iterations,radius);
        Draw1(points,color);
    }
    public static void DrawPoint(Vector3 point, Color color, float length)
    {
        var start = point;
        for (int i = 0; i < 30; i++)
        {
            var randVec = Random.insideUnitSphere;;
            var end = point+randVec*length;
            //var duration = 0.2f;
            DrawLine(start,end,color);
        }
    }
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
        MaybeClearLines();
        lines.Add(new Line(start, end, color));
    }
    public static void DrawLineScreen(Vector2 start, Vector2 end, Color color)
    {
        MaybeClearLines();
        linesScreen.Add(new Line(start, end, color));
    }

    private static void Draw1(Vector3[] points, Color color)
    {
        var length = points.Length;
        if (length < 2) return;
        var p0 = points[0];
        for (int i = 1; i < length; i++)
        {
            var p1 = points[i];
            DrawLine(p0,p1,color);
            p0 = p1;
        }
        DrawLine(points[0],points[length-1],color);
    }
    private static Vector3[] CirclePoints(Vector3 center, int iterations, float radius)
    {
        iterations = Mathf.Clamp(iterations,3,int.MaxValue);
        var result = new Vector3[iterations];
        var p0 = Vector3.right * radius;
        result[0] = p0;
        var angle = 360f/iterations;
        for (int i = 1; i < iterations; i++)
        {
            var rotation = Quaternion.Euler(0,angle*i,0);
            result[i] = (rotation*p0);
        }
        for (int i = 0; i < iterations; i++) result[i] += center;
        return result;
    }

    static GLLines()
    {
        if (material != null) return;
        var shader = Resources.Load<Shader>("Empty");
        material = new Material(shader);
        Camera.onPostRender += Render;
    }
    private static void Render(Camera camera)
    {
        if (material == null) return;
        MaybeClearLines();
        material.SetPass(0);

        GL.PushMatrix();
        DrawLines(lines);
        GL.PopMatrix();

        GL.PushMatrix();
        GL.LoadPixelMatrix();
        DrawLines(linesScreen);
        GL.PopMatrix();
    }
    private static void DrawLines(List<Line> list)
    {
        if (list.Count == 0) return;
        GL.Begin(GL.LINES);
        foreach (var line in list)
        {
            GL.Color(line.color);
            GL.Vertex(line.start);
            GL.Vertex(line.end);
        }
        GL.End();
    }
    private static void MaybeClearLines()
    {
        if (clearedFrame == Time.frameCount) return;
        clearedFrame = Time.frameCount;
        lines.Clear();
        linesScreen.Clear();
    }
    private struct Line
    {
        public Vector3 start;
        public Vector3 end;
        public Color color;

        public Line(Vector3 start, Vector3 end, Color color)
        {
            this.start = start;
            this.end = end;
            this.color = color;
        }
    }
}