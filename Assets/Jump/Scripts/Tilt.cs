using UnityEngine;

public class Tilt : MonoBehaviour
{
    public Vector2 vec;
    public Vector2 globalDirection;
    public float angle;

    private void Update()
    {
        if (vec != Vector2.zero)
        {
            var duno = new Vector3(vec.y,0f,-vec.x);
            transform.rotation = Quaternion.AngleAxis(vec.magnitude,duno) * transform.parent.rotation;
        }
        else
        {
            var axis1 = new Vector2(globalDirection.y,-globalDirection.x);
            var axis2 = new Vector3(axis1.x,0,axis1.y);
            transform.rotation = Quaternion.AngleAxis(angle,axis2) * transform.parent.rotation;

            // var axis1 = new Vector2(globalDirection.y,-globalDirection.x);
            // var axis2 = new Vector2(axis1.x,axis1.y);
            // var axis3 = new Vector3(axis2.x,0,axis2.y);
            // transform.rotation = Quaternion.AngleAxis(angle,axis3) * transform.parent.rotation;
        }
    }
}
