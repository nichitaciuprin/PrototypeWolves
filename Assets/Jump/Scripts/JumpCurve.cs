using UnityEngine;

public class JumpCurve : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float curve;

    public static JumpCurve Create(Vector3 start, Vector3 end, float curve)
    {
        var rootObject = new GameObject("JumpCurve");
        var component = rootObject.AddComponent<JumpCurve>();
        rootObject.AddComponent<JumpCurveDraw>();
        component.start = start;
        component.end = end;
        component.curve = curve;
        return component;
    }
    public Vector3 Evaluate(float x)
    {
        Vector3 result;
        var vec1 = end-start;
        var vec2 = new Vector2(vec1.x,vec1.z);
        var vec3 = new Vector2(vec2.magnitude,vec1.y);
        var t = x/vec3.x;
        var vec4 = vec2*t;
        result.x = vec4.x;
        result.y = YProjected(vec3,x);
        result.z = vec4.y;
        return result+start;
    }
    public Vector3 Velocity(float x, float g)
    {
        var velocity = end-start;
        velocity.y = 0;
        velocity.Normalize();
        velocity *= HorSpeed(g);
        velocity.y = VertSpeed(x,g);
        return velocity;
    }
    public float VertSpeed(float x, float g)
    {
        var vec1 = end-start;
        var vec2 = new Vector2(vec1.x,vec1.z);
        var vec3 = new Vector2(vec2.magnitude,vec1.y);
        var dist = DistanceToExtreme(vec3,x);
        return g*dist/HorSpeed(g);
    }
    public float HorSpeed(float g)
    {
        var a = Mathf.Abs(curve);
        var t = Mathf.Sqrt(2*a/g);
        return 1/t;
    }
    private float YProjected(Vector2 end, float x)
    {
        var a = -Mathf.Abs(curve);
        var b = end.y/end.x - a*end.x;
        return a*x*x + b*x;
    }
    private float DistanceToExtreme(Vector2 end, float x)
    {
        var a = -Mathf.Abs(curve);
        var b = end.y/end.x - a*end.x;
        //return -((b/(2*a))+x);
        return -b/a/2-x;
    }
}
