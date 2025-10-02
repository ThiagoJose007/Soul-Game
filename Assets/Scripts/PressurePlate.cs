using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Sprite spritePressed; // Sprite da placa pressionada
    [SerializeField] private Sprite spriteReleased; // Sprite da placa normal

    [Header("Settings")]
    [Tooltip("A tag do objeto que pode ativar esta placa (ex: Grabbable)")]
    [SerializeField] private string activatingTag = "Grabbable";

    [Header("Events")]
    public UnityEvent onPlatePressed;
    public UnityEvent onPlateReleased;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteReleased; // Garante que comece no estado normal
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou tem a tag correta
        if (other.CompareTag(activatingTag))
        {
            Debug.Log("Placa de pressão ATIVADA por: " + other.name);
            spriteRenderer.sprite = spritePressed; // Muda o visual
            onPlatePressed.Invoke(); // "Grita" que foi pressionada!
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o objeto que saiu tem a tag correta
        if (other.CompareTag(activatingTag))
        {
            Debug.Log("Placa de pressão DESATIVADA.");
            spriteRenderer.sprite = spriteReleased; // Volta o visual
            onPlateReleased.Invoke(); // "Grita" que foi solta!
        }
    }
}
