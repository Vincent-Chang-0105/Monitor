using System;

public class PlayerEvents
{
    public event Action onDisablePlayerMovement;
    public void DisablePlayerMovement()
    {
        if (onDisablePlayerMovement != null)
        {
            onDisablePlayerMovement();
        }
    }

    public event Action onEnablePlayerMovement;
    public void EnablePlayerMovement()
    {
        if (onEnablePlayerMovement != null)
        {
            onEnablePlayerMovement();
        }
    }

    public event Action onDisablePlayerUI;
    public void DisablePlayerUI()
    {
        if (onDisablePlayerUI != null)
        {
            onDisablePlayerUI();
        }
    }

    public event Action onEnablePlayerUI;
    public void EnablePlayerUI()
    {
        if (onEnablePlayerUI != null)
        {
            onEnablePlayerUI();
        }
    }

}
