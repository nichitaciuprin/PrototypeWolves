using UnityEngine;

public class Test1 : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 rotation;
    private void LateUpdate()
    {
        transform.position = target.position;
        transform.position += offset;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
