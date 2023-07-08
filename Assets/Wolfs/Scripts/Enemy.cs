using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Vector3 lookAtTarget;
    public JumpRotation jumpRotation;
    public Movement movement;
    public JumpProcess jumpProcess;
    public Ragdoll ragdoll;
    public bool disableJump;
    private bool jumping;

    public void EnableRagdoll()
    {
        ragdoll.enabled = true;
    }
    public void Jump(Vector3 target)
    {
        StartCoroutine(JumpAsync(target));
    }
    private void Update()
    {
        movement.lookTarget = lookAtTarget;
    }
    private IEnumerator JumpAsync(Vector3 target)
    {
        if (disableJump) yield break;
        if (jumping) yield break;

        jumping = true;
        movement.enabled = false;

        jumpProcess = JumpProcess.Create(transform.position,target,0.05f,10f,transform);
        jumpRotation.jumpProcess = jumpProcess;
        while(jumpProcess != null) yield return null;

        jumping = false;
        if (!ragdoll.enabled)
            movement.enabled = true;
    }
    public Vector3 Velocity()
    {
        if (jumpProcess)
            return jumpProcess.Velocity();
        return movement.velocity;
    }
}