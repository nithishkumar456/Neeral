using UnityEngine;

public class CameraFollowFixed : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(2f, 7f, -5f);
    [SerializeField] private float followSpeed = 10f;

    private Quaternion fixedRotation;

    private void Start()
    {
        fixedRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.rotation = fixedRotation;
    }
}
