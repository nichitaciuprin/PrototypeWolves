using UnityEngine;

public class Example1 : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;
    public Transform p3;
    public Transform p4;

    private void Update()
    {
        GLLines.DrawLine(p0.position,p1.position,Color.green);
        GLLines.DrawLine(p1.position,p2.position,Color.green);
        GLLines.DrawLine(p2.position,p3.position,Color.green);
        GLLines.DrawLine(p3.position,p4.position,Color.green);

        transform.Rotate(Vector3.up*Time.deltaTime*100);
    }
}
