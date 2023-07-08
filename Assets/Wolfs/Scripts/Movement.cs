using UnityEngine;

public class Movement : MonoBehaviour
{
    public static float acc = 30f;
    public Transform target;
    [HideInInspector] public Vector3 velocity;
    [HideInInspector] public Vector3? lookTarget;
    private Vector3 lastPos;
    public Orbit orbit;

    private void Update()
    {
        var sphere = Sphere();
        if (sphere == null) return;
        GLLines.DrawCircle(sphere.Value.position,20,sphere.Value.radius,Color.black);
    }
    private void FixedUpdate()
    {
        var deltaTime = Time.fixedDeltaTime;
        if (orbit != null) orbit.ManualUpdate(deltaTime);
        var targetVel = target.position - lastPos;
        targetVel /= deltaTime;
        transform.position = Point.Move(deltaTime,transform.position,ref velocity,target.position,targetVel,acc,Sphere());
        lastPos = target.position;
        Rotate();
    }
    private void Rotate()
    {
        if (lookTarget == null) return;
        if (Vector3.Distance(lookTarget.Value,transform.position) < 0.1f) return;
        var direction = lookTarget.Value-transform.position;
        var targetRotation = Quaternion.LookRotation(direction,Vector3.up);
        var angle = Quaternion.Angle(transform.rotation,targetRotation);
        var delta = angle*Time.deltaTime*6;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetRotation,delta);
    }
    private Sphere? Sphere()
    {
        if (lookTarget == null) return null;
        Sphere result;
        result.position = lookTarget.Value;
        result.radius = 5;
        return result;
    }
}