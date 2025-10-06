using UnityEngine;

public class EnemyAttackEventRelay : MonoBehaviour
{
    [Header("Hand hitboxes")]
    public AttackHitbox rightHandHitbox;
    public AttackHitbox leftHandHitbox;

    // Right hand events
    public void EnableRightHandDamage()  { if (rightHandHitbox) rightHandHitbox.EnableDamage(); }
    public void DisableRightHandDamage() { if (rightHandHitbox) rightHandHitbox.DisableDamage(); }

    // Left hand events
    public void EnableLeftHandDamage()   { if (leftHandHitbox) leftHandHitbox.EnableDamage(); }
    public void DisableLeftHandDamage()  { if (leftHandHitbox) leftHandHitbox.DisableDamage(); }
}
