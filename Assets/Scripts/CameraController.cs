using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;                          // Jogador
    public Vector3 offset = new Vector3(0, 5f, -10f);  // Offset inicial
    public float smoothSpeed = 0.125f;                // Suavização
    private Vector3 initialOffsetDirection;

    private RagdollActivator r; 

    void Start()
    {
        if (target != null)
        {
            initialOffsetDirection = transform.position - target.position;

            r = target.GetComponent<RagdollActivator>();
        }
    }

    void LateUpdate()
    {
        if (target == null || (r!= null && r.isDead))
            return;

        Vector3 desiredPosition = target.position + initialOffsetDirection;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
