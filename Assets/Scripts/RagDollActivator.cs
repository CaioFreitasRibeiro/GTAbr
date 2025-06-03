using UnityEngine;
using UnityEngine.SceneManagement;

public class RagdollActivator : MonoBehaviour
{
    public Animator animator;
    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;
    public Collider mainCollider;
    public MonoBehaviour playerController;
    public PlayerController pc;
    public float timeToReload = 2f;

    private bool isDead = false;

    void Start()
    {
        SetRagdollState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        SetRagdollState(true);

        animator.enabled = false;
        mainCollider.enabled = false;
        playerController.enabled = false;
        pc.canMove = false;

        Invoke("ReloadScene", timeToReload);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetRagdollState(bool state)
    {
        foreach (Rigidbody rb in ragdollBodies)
        {
            rb.isKinematic = !state;
        }

        foreach (Collider col in ragdollColliders)
        {
            col.enabled = state;
        }

        animator.enabled = !state;
    }


}
