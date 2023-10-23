using System.Atomic.Interfaces;
using GamePlay.Components.Interfaces;

namespace GamePlay.Components
{
    public class ShootComponent : IShootable
    {
        private readonly IAtomicAction OnPressedShootButton;
    
        public ShootComponent(IAtomicAction onPressedShootButton)
        {
            OnPressedShootButton = onPressedShootButton;
        }
    
        void IShootable.PressedFireButton()
        {
            OnPressedShootButton?.Invoke();
        }
    }
}
