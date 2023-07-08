using UnityEngine;

public struct Point
{
    public Vector3 pos;
    public Vector3 vel;
    private float am;

    public static Vector3 Move(float T, Vector3 A_pos, ref Vector3 A_vel, Vector3 B_pos, Vector3 B_vel, float am, Sphere? obstacle)
    {
        var A = new Point(A_pos,A_vel,am);
        var B = new Point(B_pos,B_vel,am);
        A = Move(T,A,B);
        A_vel = A.vel;
        return A.pos;
    }
    public Point(Vector3 pos, Vector3 vel, float am)
    {
        this.pos = pos;
        this.vel = vel;
        this.am = am;
    }
    private static Point Move(float T, Point A, Point B)
    {
        var vel1 = A.vel;
        var targetVelocity = TargetVelocity(A,B);
        var vel2 = Adapter.MoveTowards(vel1,targetVelocity,A.am*T);
        A.pos += (vel1+vel2)/2f*T;
        A.vel = vel2;
        A.pos = Snap(A.pos,B.pos);
        return A;
    }
    private static Vector3 TargetVelocity(Point A, Point B)
    {
        var diff = B.pos - A.pos;
        if (diff == Adapter.zero) return Adapter.zero;
        var diffNorm = Adapter.Normalized(diff);
        var dist = Adapter.Magnitude(diff);
        var targetSpeed = Adapter.Sqrt(dist*A.am*2f);
        return diffNorm*targetSpeed+B.vel;
    }
    private static Vector3 Snap(Vector3 a, Vector3 b) => Vector3.Distance(a,b) > 1E-2f ? a : b;
}