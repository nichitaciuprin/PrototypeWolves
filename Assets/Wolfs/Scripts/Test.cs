using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform rayObject;
    public Transform circleObject;

    private void Update()
    {
        Ray ray;
        ray.from = rayObject.position;
        ray.dir = rayObject.up;

        Sphere circle;
        circle.position = circleObject.position;
        circle.radius = circleObject.localScale.x/2;

        var result = MyPhysics.Raycast(ray,circle);

        if (result == null)
        {
            Debug.DrawRay(ray.from,ray.dir*1000,Color.green);
        }
        else
        {
            Debug.DrawLine(ray.from,result.Value.point,Color.red);
        }
    }
}
