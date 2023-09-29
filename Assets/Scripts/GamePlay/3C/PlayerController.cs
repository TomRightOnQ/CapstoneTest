using UnityEngine;

/// <summary>
/// Attching this component to the player for control
/// </summary>

public class PlayerController : MonoBehaviour 
{
    // Lock input from keyboard
    private bool bInputLocked = false;

    // Lock attack movement
    // ToDo: Add more flags...

    // Lock all movement
    private bool bMovementLocked = false;
    // Lock all active mmovement
    private bool bMovable = false;

    // Stop all movement of the player
    public void StopPlayerMovement()
    {
        
    }

    // All external classes attampting to move the player shall call this method
    // movement: vector indicating the movement
    // bAvtiveMove: if this movement comes from the player input
    public void MovePlayer(Vector3 movement, bool bActiveMove)
    {
        //Player's position is locked
        if (!bMovementLocked) {
            return;
        }
        // Active move banned
        if (bActiveMove && !bMovable)
        {
            return;
        }
    }

    // Force move of the player
    public void RelocatePlayer(Vector3 targetPos)
    {
        
    }

    // Reset flags
    public void ResetPlayerController()
    {
    
    }

    // Change input and lock state
    public void ChangePlayerInputState(bState = false)
    {
        bInputLocked = bState;
    }

    public void ChangePlayerMovementState(bState = false)
    {
        bMovementLocked = bState;
    }

    public void ChangePlayerActiveMovementState(bState = false)
    {
        bMovable = bState;
    }

    // Player movement code
}