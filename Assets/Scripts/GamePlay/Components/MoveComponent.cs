using System.Atomic.Interfaces;
using GamePlay.Components.Interfaces;
using UnityEngine;

namespace GamePlay.Components
{
    public sealed class MoveComponent : IMoveable
    {
        private readonly IAtomicAction<Vector3> onMove;

        public MoveComponent(IAtomicAction<Vector3> onMove)
        {
            this.onMove = onMove;
        }

        void IMoveable.Move(Vector3 direction)
        {
            this.onMove.Invoke(direction);
        }
    }
}
