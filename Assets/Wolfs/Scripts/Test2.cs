using UnityEngine;

public class Test2 : MonoBehaviour
{
    public Transform p0;
    public Transform p1;

    public float test1 = 0f;
    public float test2 = 0f;

    private void Update()
    {
        GLLines.DrawLine(Vector3.zero,p0.position,Color.green);
        GLLines.DrawLine(Vector3.zero,p1.position,Color.yellow);
        //var result = Vector3.RotateTowards(p0.position,p1.position,test1,test2);
        var result = Vector3.Slerp(p0.position,p1.position,test1);
        GLLines.DrawLine(Vector3.zero,result,Color.magenta);
    }
}
