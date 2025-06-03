using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public bool canMove = true;
    public RagdollActivator RagDoll;
    public CameraController mainCamera; // camera principal do jogador, mudar quando entrar no carro

    private void Start()
    {
        mainCamera = Object.FindAnyObjectByType<CameraController>();

        RagDoll = GetComponent<RagdollActivator>();
    }

    private void Update()
    {
        if (!canMove || RagDoll == null || RagDoll.IsRagdollActive()) return;

        MovePlayer();

        SwapToCar();
    }

    private void MovePlayer()
    {
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

    private void SwapToCar()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressionou E para entrar no carro");

            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Vector3 rayOffset = new Vector3(0, 0.5f, 0); // Ajuste de altura do raio
            Debug.DrawRay(ray.origin + rayOffset, ray.direction, Color.red, 2f); // Visualização do raio

            if (Physics.Raycast(ray.origin + rayOffset, ray.direction, out hit))
            {
                if (hit.collider.CompareTag("Car"))
                {
                    // Desativa o controlador do jogador
                    canMove = false;

                    // Ativa o controlador do carro
                    CarControler carController = hit.collider.GetComponent<CarControler>();
                    if (carController != null)
                    {
                        carController.enabled = true;
                        carController.gameObject.SetActive(true);
                    }

                    mainCamera.target = hit.transform; // Desativa a câmera do jogador

                    // Destrói o personagem ("ele está no carro agora")
                    Destroy(gameObject);
                }
            }
        }
    }
}
