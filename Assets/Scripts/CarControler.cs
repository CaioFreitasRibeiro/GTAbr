using Unity.VisualScripting;
using UnityEngine;

public class CarControler : MonoBehaviour
{
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    public float motorForce = 1500f;
    public float maxSteerAngle = 30f;

    public GameObject playerPrefab; // Prefab do jogador a ser instanciado
    public CameraController mainCamera; // Referência à câmera principal do jogador

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Object.FindAnyObjectByType<CameraController>();
        }
    }

    private void Update()
    {
        // Verifica se o jogador pressionou a tecla E para sair do carro
        SwapToplayer();
    }

    void FixedUpdate()
    {
        float motor = Input.GetAxis("Vertical") * motorForce;
        float steer = Input.GetAxis("Horizontal") * maxSteerAngle;

        // Direção
        frontLeft.steerAngle = steer;
        frontRight.steerAngle = steer;

        // Tração
        rearLeft.motorTorque = motor;
        rearRight.motorTorque = motor;

        UpdateWheels();

        if (Input.GetKey(KeyCode.Space))
        {
            rearLeft.brakeTorque = 3000f;
            rearRight.brakeTorque = 3000f;
        }
        else
        {
            rearLeft.brakeTorque = 0f;
            rearRight.brakeTorque = 0f;
        }
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeft, frontLeftTransform);
        UpdateSingleWheel(frontRight, frontRightTransform);
        UpdateSingleWheel(rearLeft, rearLeftTransform);
        UpdateSingleWheel(rearRight, rearRightTransform);
    }

    void UpdateSingleWheel(WheelCollider collider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        collider.GetWorldPose(out pos, out rot);
        trans.position = pos;
        trans.rotation = rot;
    }

    private void SwapToplayer()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Instanciar novo objeto do jogador
            Vector3 offset = new Vector3(1.5f, 0, 0); // Ajuste de posição para o jogador sair do carro
            GameObject currentPlayer = Instantiate(playerPrefab, transform.position + offset, transform.rotation); // O Player "saiu" do carro

            mainCamera.target = currentPlayer.transform; // Atualiza a câmera para seguir o novo jogador 

            // desativar esse script
            this.enabled = false;
        }
    }
}
