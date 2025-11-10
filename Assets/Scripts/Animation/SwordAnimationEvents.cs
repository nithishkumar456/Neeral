using UnityEngine;

public class SwordAnimationEvents : MonoBehaviour
{
    [SerializeField] private SwordHitbox swordHitbox;

    // Called by animation events
    public void EnableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.EnableHitbox();
    }
    public void DisableHitbox()
    {
        if (swordHitbox != null)
            swordHitbox.DisableHitbox();
    }
}
