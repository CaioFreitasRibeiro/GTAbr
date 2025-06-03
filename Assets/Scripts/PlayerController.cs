using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public bool canMove = true;

    private void Update()
    {
        if (!canMove) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);

        if (move.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
            transform.forward = move; // Rotaciona para direção do movimento
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
