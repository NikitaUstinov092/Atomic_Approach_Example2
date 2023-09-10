using System.Atomic.Interfaces;

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
