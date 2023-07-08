using UnityEngine;

public class MovementRotation : MonoBehaviour
{
    public Movement movement;
    public Tilt tilt;
    private Vector3 oldVel = Vector3.zero;
    private Vector3 newVel = Vector3.zero;
    private Vector3 smoothDampCurentPos = Vector3.zero;
    private Vector3 smoothDampVel = Vector3.zero;

    private void FixedUpdate()
    {
        if (!movement.enabled) return;
        newVel = movement.velocity;
        var diff = (newVel - oldVel)/Time.fixedDeltaTime;
        oldVel = movement.velocity;
        smoothDampCurentPos = Vector3.SmoothDamp(smoothDampCurentPos,diff,ref smoothDampVel,0.5f);
        tilt.vec = new Vector2(smoothDampCurentPos.x,smoothDampCurentPos.z);
        tilt.vec *= 2;
    }
}