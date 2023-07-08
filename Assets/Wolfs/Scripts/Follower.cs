using UnityEngine;

public class Follower : MonoBehaviour
{
    public Target target;
    public float acc;
    public Transform obstacleTrans;
    private Vector3 velocity;

    private void FixedUpdate()
    {
        var obstacle = obstacleTrans == null ? (Sphere?)null : new Sphere(obstacleTrans.position, 15);
        transform.position = Point.Move(Time.fixedDeltaTime,transform.position,ref velocity,target.transform.position,target.velocity,acc,obstacle);
        //transform.position = Point.Move(2f,transform.position,ref velocity,target.transform.position,target.velocity,acc,obstacle);
    }
}
