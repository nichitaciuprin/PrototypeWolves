using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public Vector3 center2;
    private Vector3 centerOffset = Vector3.forward;

    private const float speed_min = 2f;
    private const float speed_max = 10f;
    private const float dist_min = 7f;
    private const float dist_max = 12f;
    private static bool random = true;

    [SerializeField] [Range(speed_min,speed_max)] private float speed;
    [SerializeField] [Range(dist_min,dist_max)] private float distance;
    [SerializeField] private bool isRight;

    private void Update()
    {
        var color = Color.red;
        var radius = 0.5f;
        var pos = transform.position+(Vector3.up*0.001f);
        var iterations = 8;
        GLLines.DrawLine(pos,target.position,color);
        GLLines.DrawCircle(pos,iterations,radius,color);
        GLLines.DrawCross(pos,radius,color);
    }
    private IEnumerator Start()
    {
        while (true)
        {
            if (random)
            {
                Init();
                var seconds = Random.Range(5,15);
                yield return new WaitForSeconds(seconds);
            }
            else
            {
                yield return null;
            }
        }
    }
    public void ManualUpdate(float deltaTime)
    {
        if (speed == 0) return;
        if (distance == 0) return;
        centerOffset.Normalize();
        centerOffset *= distance;
        centerOffset = NewPosition(centerOffset,deltaTime);
        transform.position = target.position + centerOffset;
    }
    private void Init()
    {
        centerOffset = Random.insideUnitSphere;
        centerOffset.y = 0;
        distance = Random.Range(dist_min,dist_max);
        speed = Random.Range(speed_min,speed_max);
        isRight = Random.Range(0,2) == 0 ? true : false;
    }
    private Vector3 NewPosition(Vector3 vector, float time)
    {
        var distance = vector.magnitude;
        var y = 360f*time*speed/2/Mathf.PI/distance;
        y *= (isRight ? 1 : -1);
        var rot = Quaternion.Euler(0,y,0);
        return rot*vector;
    }
    public Vector2 Velocity()
    {
        var vec = target.position - transform.position;
        var vec2 = new Vector2(-vec.z,vec.x);
        vec2 *= (isRight ? 1 : -1);
        vec2.Normalize();
        vec2 *= speed;
        return vec2;
    }
}