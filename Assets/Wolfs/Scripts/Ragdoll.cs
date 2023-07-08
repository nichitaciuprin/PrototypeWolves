using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public Tilt tilt1;
    public Tilt tilt2;
    public Rigidbody rb;
    public Enemy enemy;
    public Movement movement;
    public JumpRotation jumpRotation;

    public void OnEnable()
    {
        rb.isKinematic = false;
        rb.velocity = enemy.Velocity();

        movement.enabled = false;
        jumpRotation.enabled = false;
        tilt1.enabled = false;
        tilt2.enabled = false;

        if (enemy.jumpProcess)
            Destroy(enemy.jumpProcess.gameObject);
        
        enemy.disableJump = true;
    }
}
