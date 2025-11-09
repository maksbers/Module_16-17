using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private string _horizontalInputName = "Horizontal";
    private string _verticalInputName = "Vertical";

    private Vector3 _inputAxis;

    public Vector3 ReadInput()
    {
        _inputAxis = new Vector3(Input.GetAxisRaw(_horizontalInputName), 0, Input.GetAxisRaw(_verticalInputName));
        return _inputAxis;
    }
}
