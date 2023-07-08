using UnityEngine;

public class MouseControllerHorizontal : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        var direction = InputDirection2();
        var x = direction.x;
        var mouseSense = 1f;
        var mouseX = x * mouseSense;
        this.transform.Rotate(Vector3.up * mouseX);
    }
    private static Vector2 InputDirection2()
    {
        var x = Input.GetAxisRaw("Mouse X");
        var y = Input.GetAxisRaw("Mouse Y");
        return new Vector2(x,y);
    }
}
