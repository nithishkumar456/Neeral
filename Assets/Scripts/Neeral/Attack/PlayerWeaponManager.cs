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
        currentWeapon?.EnableHitbox();
    }

    public void DisableWeaponHitbox()
    {
        currentWeapon?.DisableHitbox();
    }

    public void SetCurrentWeapon(WeaponHitbox newWeapon)
    {
        currentWeapon = newWeapon;
    }
}
