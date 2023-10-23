using System;
using GamePlay.Custom.GameMachine;
using UnityEngine;

namespace GamePlay.Custom.Input
{
    public sealed class MovementInput : MonoBehaviour, IUpdateListener
    {
        public event Action<Vector3> MovementDirectionChanged;
        
        private Vector3 _previousMovement;
        
        [SerializeField] 
        private KeyCode _leftKey;
        
        [SerializeField] 
        private KeyCode _rightKey;
        
        [SerializeField] 
        private KeyCode _forwardKey;
       
        [SerializeField] 
        private KeyCode _backKey;

        void IUpdateListener.Update()
        {
            var movement = new Vector3();
            
            if (UnityEngine.Input.GetKey(_forwardKey))
            {
                movement.z += 1;
            }
            
            if (UnityEngine.Input.GetKey(_backKey))
            {
                movement.z -= 1;
            }
            
            if (UnityEngine.Input.GetKey(_rightKey))
            {
                movement.x += 1;
            }
            
            if (UnityEngine.Input.GetKey(_leftKey))
            {
                movement.x -= 1;
            }

            if (movement != _previousMovement)
            {
                _previousMovement = movement;
                MovementDirectionChanged?.Invoke(movement.normalized);
            }
        }
    }
}