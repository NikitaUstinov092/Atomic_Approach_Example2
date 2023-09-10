using GamePlay.Components.Interfaces;
using GamePlay.Custom.GameMachine;
using UnityEngine;

namespace GamePlay.Custom.Input
{
    public sealed class MoveInput : MonoBehaviour, IUpdateListener
    {
        [SerializeField]
        private Entity.Entity _entity;
        
        [SerializeField] 
        private KeyCode _leftKey;
        [SerializeField] 
        private KeyCode _rightKey;
        [SerializeField] 
        private KeyCode _forwardKey;
        [SerializeField] 
        private KeyCode _backKey;
        
        private Vector3 _direction;
        
        void IUpdateListener.Update()
        {
            if (UnityEngine.Input.GetKey(_leftKey))
            {
                _direction = Vector3.left;
                ApplyMove(_direction);
            }
            else if (UnityEngine.Input.GetKey(_rightKey))
            {
                _direction = Vector3.right;
                ApplyMove(_direction);
            }
            else if (UnityEngine.Input.GetKey(_forwardKey))
            {
                _direction = Vector3.forward;
                ApplyMove(_direction);
            }
            else if (UnityEngine.Input.GetKey(_backKey))
            {
                _direction = Vector3.back;
                ApplyMove(_direction);
            }
        }

        private void ApplyMove(Vector3 direction)
        {
            if (!_entity.TryGet(out IMoveable moveComponent))
                return;
            
            moveComponent.Move(direction);
        }
    }
}

