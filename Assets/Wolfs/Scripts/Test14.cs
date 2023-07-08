using UnityEngine;

public class Test14 : MonoBehaviour
{
    public Camera cam;
    private bool slowmotion = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            slowmotion = !slowmotion;

        var defaultTime = 1.0f;
        if (slowmotion)
            Time.timeScale = defaultTime/3;
        else
            Time.timeScale = defaultTime;

        var ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        var result = Physics.Raycast(ray, out RaycastHit hit);
        if (!result) return;
        var start = hit.point;
        var end = start+(hit.normal/5);
        GLLines.DrawLine(start,end,Color.cyan);
        if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        var maybeEnemy = hit.collider.GetComponentInParent<Enemy>();
        if (!maybeEnemy) return;
        maybeEnemy.ragdoll.enabled = true;
        var enemyRb = maybeEnemy.GetComponent<Rigidbody>();
        var dir1 = -hit.normal;
        var dir2 = (hit.point-cam.transform.position).normalized;
        var dir3 = (dir2+Vector3.up).normalized;
        var dir = dir3;
        var power = 3f;
        var force = dir*power;
        enemyRb.AddForceAtPosition(force,hit.point,ForceMode.Impulse);
    }
}
