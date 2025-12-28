using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    private WeaponHitbox currentWeapon;

    private void Awake()
    {
        EquipWeapon(GetComponentInChildren<WeaponHitbox>());
    }

    public void EquipWeapon(WeaponHitbox newWeapon)
    {
        currentWeapon = newWeapon;
    }

    public void EnableWeaponHitbox()
    {
        currentWeapon?.EnableHitbox();
    }

    public void DisableWeaponHitbox()
    {
        currentWeapon?.DisableHitbox();
    }
}
