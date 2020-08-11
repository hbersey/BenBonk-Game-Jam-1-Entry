using UnityEngine;

public static class InputUtil
{
    private const float MovementThreshold = 0.01f;
    public static Vector2 GetMovementRaw() =>
        new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

    public static bool IsMoving() => GetMovementRaw().sqrMagnitude > MovementThreshold;
}