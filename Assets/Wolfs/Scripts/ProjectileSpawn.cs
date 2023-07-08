using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{
    public GameObject projectilePrefab;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;

        //var projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        //var rigidbody = projectile.GetComponent<Rigidbody>();
        //rigidbody.velocity = transform.forward*20;
    }
}
