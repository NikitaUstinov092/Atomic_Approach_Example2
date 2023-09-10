using GamePlay.Components.Interfaces;
using GamePlay.Custom.GameMachine;
using UnityEngine;

namespace GamePlay.Custom.Input
{
    public class RotationInput : MonoBehaviour, IUpdateListener
    {
        [SerializeField]
        private Entity.Entity _entity;

        void IUpdateListener.Update()
        {
            if (!_entity.TryGet(out IRotatable rotatable))
                return;
            
            var screenPos = UnityEngine.Input.mousePosition;
            rotatable.RotateInDirection(screenPos);
        }
    }
}

