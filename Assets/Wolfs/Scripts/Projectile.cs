using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 position;
    private Vector3 velocity;
    private Vector3 angularVelocity;

    private int ignoreFrame;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,10);
    }
    private void FixedUpdate()
    {
        Save();        
    }
    private void OnCollisionEnter(Collision collision)
    {
        var maybeEnemy = collision.collider.GetComponentInParent<Enemy>();
        if (!maybeEnemy) return;

        //var enemyRb = maybeEnemy.GetComponent<Rigidbody>();
        //maybeEnemy.ragdoll.enabled = true;
        //Destroy(gameObject);
        //enemyRb.AddForceAtPosition(rb.velocity*20,transform.position,ForceMode.Force);

        if (maybeEnemy.ragdoll.enabled)
        {
            if (ignoreFrame == Time.frameCount) return;
            Destroy(gameObject);
        }
        else
        {
            ignoreFrame = Time.frameCount;
            maybeEnemy.ragdoll.enabled = true;
            Load();
        }
    }
    private void Save()
    {
        position = transform.position;
        velocity = rb.velocity;
        angularVelocity = rb.angularVelocity;
    }
    private void Load()
    {
        transform.position = position;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }
}