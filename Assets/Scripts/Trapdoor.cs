using UnityEngine;

public class Trapdoor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float openAngle = -90f; // Ângulo para quando estiver aberto

    private Vector3 closeRotation; // Guarda a rotação original
    private Collider2D trapdoorCollider;

    private void Start()
    {
        // Guarda a rotação inicial para saber como fechar
        closeRotation = transform.eulerAngles;
        
        // Pega o componente de colisão
        trapdoorCollider = GetComponent<Collider2D>();
    }

    // Função pública que a alavanca vai chamar
    public void Open()
    {
        Debug.Log("Alçapão abrindo!");
        
        // Gira o alçapão para o ângulo de abertura
        transform.eulerAngles = new Vector3(0, 0, openAngle);
        
        // Desativa o colisor para que os objetos possam cair
        if (trapdoorCollider != null)
        {
            trapdoorCollider.enabled = false;
        }
    }

    // Função pública para fechar
    public void Close()
    {
        Debug.Log("Alçapão fechando!");

        // Volta para a rotação original
        transform.eulerAngles = closeRotation;
        
        // Reativa o colisor
        if (trapdoorCollider != null)
        {
            trapdoorCollider.enabled = true;
        }
    }
}
