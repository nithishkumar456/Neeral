using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private WeaponHitbox currentWeapon;

    private void Awake()
    {
        currentWeapon = GetComponentInChildren<WeaponHitbox>();
    }

    public void EnableWeaponHitbox()
    {
        if (currentWeapon != null)
            currentWeapon.EnableHitbox();
    }

    public void DisableWeaponHitbox()
    {
        if (currentWeapon != null)
            currentWeapon.DisableHitbox();
    }

    // Call this when switching weapons
    public void SetCurrentWeapon(WeaponHitbox newWeapon)
    {
        currentWeapon = newWeapon;
    }
}
