using UnityEngine;
using UnityEngine.Events;
public class Lever : MonoBehaviour, IInteractable
{
    [Header("Visuals")]
    public Sprite spriteLeverOn; // Sprite da alavanca ativada
    public Sprite spriteLeverOff; // Sprite da alavanca desativada

    [Header("Logic")]
    private bool isOn = false;
    private SpriteRenderer spriteRenderer;

    [Header("Events")]
    public UnityEvent onLeverActivate; // Evento chamado ao LIGAR
    public UnityEvent onLeverDeactivate; // Evento chamado ao DESLIGAR

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = spriteLeverOff; // Garante que começa desligada
    }

    // Esta é a função exigida pelo contrato "IInteractable"
    public void Interact()
    {
        Debug.Log(" Wait");
        // Inverte o estado da alavanca
        isOn = !isOn;
        
        if (isOn)
        {
            Debug.Log("Interact");
            spriteRenderer.sprite = spriteLeverOn;
            onLeverActivate.Invoke(); // "Grita" para todo mundo que foi ativada!
        }
        else
        {
            Debug.Log(" No Interact");
            spriteRenderer.sprite = spriteLeverOff;
            onLeverDeactivate.Invoke(); // "Grita" para todo mundo que foi desativada!
        }
    }
}
