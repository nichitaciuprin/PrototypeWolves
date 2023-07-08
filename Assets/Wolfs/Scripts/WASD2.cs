using UnityEngine;

public class WASD2 : MonoBehaviour
{
    public static float speed = 10;
    private void FixedUpdate()
    {
        var vec2 = Helper.InputDirectionNormalised();
        var vec3 = new Vector3(vec2.x,0,vec2.y);
        transform.position += transform.rotation*vec3*speed*Time.fixedDeltaTime;
    }
}
