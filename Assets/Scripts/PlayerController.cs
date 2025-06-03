using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public bool canMove = true;
    public RagdollActivator RagDoll;

    private void Start()
    {
        RagDoll = GetComponent<RagdollActivator>();
    }

    private void Update()
    {
        if (!canMove || RagDoll == null || RagDoll.IsRagdollActive()) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, 0, v).normalized;

        // Ativa animação de andar se estiver se movendo
        bool isWalking = move.magnitude > 0f;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = move;
        }
    }
}
