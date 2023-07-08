using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float speed;
    private Queue<Vector3> queue = new Queue<Vector3>( new []
    {
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
    });

    private void FixedUpdate()
    {
        queue.Dequeue();
        queue.Enqueue(transform.position);
        var array = queue.ToArray();
        var duno1 = array[4];
        var duno2 = array[3];
        velocity = duno1 - duno2;
        velocity /= Time.fixedDeltaTime;
        speed = velocity.magnitude;
        Debug.DrawLine(transform.position,transform.position+velocity,Color.cyan);
    }
}
