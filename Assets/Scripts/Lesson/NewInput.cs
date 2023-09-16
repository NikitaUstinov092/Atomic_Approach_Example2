using System;
using GamePlay.Custom.GameMachine;
using UnityEngine;

public class NewInput : MonoBehaviour, IUpdateListener
{
    public event Action<Vector3> MovementDirectionChanged;
        
    private Vector3 _previousMovement;

    void IUpdateListener.Update()
    {
        var movement = new Vector3();
            
        if (Input.GetKey(KeyCode.UpArrow))
        {
            movement.z += 1;
        }
            
        if (Input.GetKey(KeyCode.DownArrow))
        {
            movement.z -= 1;
        }
            
        if (Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }
            
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }

        if (movement == _previousMovement) 
            return;
        _previousMovement = movement;
        MovementDirectionChanged?.Invoke(movement.normalized);
    }
}