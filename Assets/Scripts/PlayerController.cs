using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public bool canMove = true;
    public RagdollActivator RagDoll;

    private void Start()
    {
        canMove = true;
        RagDoll = GetComponent<RagdollActivator>();
        //RagDoll.enabled = false;
    }

    private void Update()
    {
        if (!canMove) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);

        if (move.magnitude > 0.1f && RagDoll.enabled == true)
        {
            animator.SetBool("isWalking", true);
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = move; // Rotaciona para direção do movimento
        }
        else if (RagDoll.enabled == false)
        {
            animator.SetBool("isWalking", false);
        }
    }
}
