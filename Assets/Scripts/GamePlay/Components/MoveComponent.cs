using System.Atomic.Interfaces;
using GamePlay.Components.Interfaces;
using UnityEngine;

namespace GamePlay.Components
{
    public sealed class MoveComponent : IMoveAble
    {
        private readonly IAtomicAction<Vector3> OnMove;

        public MoveComponent(IAtomicAction<Vector3> onMove)
        {
            OnMove = onMove;
        }

        void IMoveAble.Move(Vector3 direction)
        {
           OnMove.Invoke(direction);
        }
    }
}
