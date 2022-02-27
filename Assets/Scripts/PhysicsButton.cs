using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float treshold = .1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPosition;
    private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;
    
    void Start()
    {
        _startPosition = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();

    }

    void Update()
    {
        if (_isPressed!=true && getValue() + treshold >= 1)
        {
            Pressed();
        }
        if (_isPressed && getValue() - treshold <= 0)
        {
            Released();
        }
    }

    private float getValue() 
    {
        var value = Vector3.Distance(_startPosition, transform.localPosition) / _joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone) value = 0;

        return Mathf.Clamp(value, -1, 1);
    }

    private void Pressed() 
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed!");
    }
    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("REleased!");
    }

}
