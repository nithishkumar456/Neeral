using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackInput : MonoBehaviour
{
    private PlayerCombat combat;

    private void Awake()
    {
        combat = GetComponent<PlayerCombat>();
    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed)
            combat.Attack();
    }
}
