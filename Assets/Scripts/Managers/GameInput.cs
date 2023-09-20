using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnAttackAction;
    public event EventHandler OnInventoryOpenAction;


    [SerializeField] FixedJoystick _fixedJoystick;

    private void Awake()
    {
        Instance = this;
    }

    public void Inventory_performed()
    {
        OnInventoryOpenAction?.Invoke(this, EventArgs.Empty);
    }
    public void Attack_performed()
    {
        OnAttackAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _fixedJoystick.Direction;

        inputVector = inputVector.normalized;

        return inputVector;

    }
}
