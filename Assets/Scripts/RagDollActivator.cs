using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    public Rigidbody[] ragdollBodies;
    public Animator animator;
    public PlayerController playerController;
    public float fallThreshold = -10f;
    public float collisionForceThreshold = 10f;

    private bool isRagdoll = false;

    void Start()
    {
        DisableRagdoll();
    }

    void Update()
    {
        if (!isRagdoll && transform.position.y < fallThreshold)
        {
            ActivateRagdoll();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isRagdoll && collision.relativeVelocity.magnitude > collisionForceThreshold)
        {
            ActivateRagdoll();
        }
    }

    public void ActivateRagdoll()
    {
        isRagdoll = true;
        animator.enabled = false;
        playerController.canMove = false;

        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = false;
        }

        Invoke("GetUpTransition", 3f); // Espera 3 segundos para levantar
    }

    void GetUpTransition()
    {
        DisableRagdoll();
        animator.enabled = true;

        // Define o estado no Animator
        animator.SetBool("isGettingUp", true);

        // Espera a animação terminar para reativar o movimento
        float getUpDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("FinishGettingUp", 2f); // Ajuste esse valor conforme a duração da animação
    }

    public void OnGetUpFinished()
    {
        animator.SetBool("isGettingUp", false);
        playerController.canMove = true;
    }

    void DisableRagdoll()
    {
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = true;
        }
    }

    void EnableMovement()
    {
        playerController.canMove = true;
    }
}
