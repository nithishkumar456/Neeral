using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private PlayerWeaponManager weaponManager;

    private void Awake()
    {
        weaponManager = GetComponent<PlayerWeaponManager>();
    }

    public void EnableHitbox()
    {
        weaponManager.EnableWeaponHitbox();
    }

    public void DisableHitbox()
    {
        weaponManager.DisableWeaponHitbox();
    }
}
