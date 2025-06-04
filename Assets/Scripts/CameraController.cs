using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;                          // Jogador
    public Vector3 offset = new Vector3(0, 5f, -10f);  // Offset inicial
    public float smoothSpeed = 0.125f;                // Suaviza��o
    private Vector3 initialOffsetDirection;

    public RagdollActivator ragdollController;      // Refer�ncia ao controle de ragdoll

    void Start()
    {
        if (target != null)
        {
            // Offset fixo em rela��o ao jogador
            initialOffsetDirection = transform.position - target.position;
        }
    }

    void Update()
    {
        if (target == null || (ragdollController != null && ragdollController.isDead))
            return;

        Vector3 desiredPosition = target.position + initialOffsetDirection;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
