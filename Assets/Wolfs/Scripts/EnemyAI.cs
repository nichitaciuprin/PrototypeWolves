using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Enemy enemy;
    public Transform player;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    enemy.EnableRagdoll();
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var target = transform.position + transform.right*10;
            enemy.Jump(target);
        }
        enemy.lookAtTarget = player.position;
    }
    private void Jump()
    {
        var target = transform.position + RandomVec();
        enemy.Jump(target);
    }
    private Vector3 RandomVec()
    {
        var x = Random.Range(-1f,1f);
        var z = Random.Range(-1f,1f);
        var vec = new Vector3(x,0,z);
        vec.Normalize();
        var length =Random.Range(4f,15f);
        return vec*length;
    }
}
