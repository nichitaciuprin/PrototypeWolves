using UnityEngine;

public class MouseControllerVertical : MonoBehaviour
{
    private float xRotation = 0f;
    public void Update()
    {
        var direction = InputDirection2();
        var y = direction.y;
        var mouseSense = 0.3f;
        var mouseY = y * mouseSense;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);;
    }
    private static Vector2 InputDirection2()
    {
        var x = Input.GetAxisRaw("Mouse X");
        var y = Input.GetAxisRaw("Mouse Y");
        return new Vector2(x,y);
    }
}
