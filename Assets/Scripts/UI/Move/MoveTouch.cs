using UnityEngine;
using UnityEngine.EventSystems;


public class MoveTouch : MonoBehaviour
{
    public FixedJoystick joystick;
    public GameObject joyStick;

    public void PointUp()
    {
        joystick.OnPointerUp(null);

        joyStick.SetActive(false);
    }   

    public void Drag()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.position = Input.mousePosition;
        joystick.OnDrag(data);
    }

    public void PointDown()
    {
        joyStick.SetActive(true);

        joyStick.transform.position = Input.mousePosition;

        joystick.OnPointerUp(null);
    }
}
