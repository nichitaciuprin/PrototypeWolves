using UnityEngine;

public class JumpProcess : MonoBehaviour
{
    public JumpCurve jumpCurve;
    public Transform moveObject;
    public float gravity;
    private float moveObjectPosition;

    public static JumpProcess Create(Vector3 start, Vector3 end, float curve, float gravity, Transform moveObject)
    {
        var rootObject = new GameObject("JumpProcess");
        var component = rootObject.AddComponent<JumpProcess>();
        component.jumpCurve = JumpCurve.Create(start,end,curve);
        component.gravity = gravity;
        component.moveObject = moveObject;
        component.jumpCurve.transform.parent = rootObject.transform;
        return component;
    }
    public Vector3 Velocity()
    {
        return jumpCurve.Velocity(moveObjectPosition,gravity);
    }
    public float Progress()
    {
        var dist = DistanceIgnoreYAxis(jumpCurve.start,jumpCurve.end);
        var t = moveObjectPosition/dist;
        return t;
    }
    public Vector2 Direction()
    {
        var dir = jumpCurve.end - jumpCurve.start;
        return new Vector2(dir.x,dir.z);
    }
    private void Update()
    {
        moveObject.position = jumpCurve.Evaluate(moveObjectPosition);
        var speed = jumpCurve.HorSpeed(gravity);
        moveObjectPosition += Time.deltaTime*speed;
        var dist = DistanceIgnoreYAxis(jumpCurve.start,jumpCurve.end);
        if (moveObjectPosition >= dist)
        {
            moveObject.position = jumpCurve.end;
            Destroy(gameObject);
            Destroy(jumpCurve.gameObject);
        }
    }
    private float DistanceIgnoreYAxis(Vector3 v0, Vector3 v1)
    {
        v0.y = v1.y;
        return Vector3.Distance(v0,v1);
    }
}
