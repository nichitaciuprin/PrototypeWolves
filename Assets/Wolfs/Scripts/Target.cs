using UnityEngine;

public class Target : MonoBehaviour
{
    public float speed;
    public Vector3 velocity;
    public bool rotate;

    private void Start()
    {
        if (rotate)
            transform.position = new Vector3(0,10);
    }
    private void FixedUpdate()
    {
        if (rotate)
        {
            transform.position = NewPosition(transform.position, speed, Time.fixedDeltaTime);
            velocity = new Vector3(-transform.position.y,transform.position.x);
            velocity.Normalize();
            velocity *= speed;
        }
        else
        {
            velocity = new Vector3(speed,0,0);
            transform.position = transform.position + velocity*Time.fixedDeltaTime;
        }
    }
    private static Vector3 NewPosition(Vector3 vector, float speed, float time)
    {
        var distance = vector.magnitude;
        var y = 360f*time*speed/2/Mathf.PI/distance;
        var rot = Quaternion.Euler(0,0,y);
        return rot*vector;
    }
}
