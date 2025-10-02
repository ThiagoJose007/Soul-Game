// Arquivo: SceneStartTrigger.cs
using UnityEngine;

public class SceneStartTrigger : MonoBehaviour
{
    [Header("Dialogue Data")]
    [Tooltip("A conversa que deve iniciar com a cena.")]
    public Dialogue dialogueToStart;

    // A função Start é chamada automaticamente pelo Unity no primeiro frame.
    void Start()
    {
        // Usa o Singleton para encontrar o Manager e iniciar o diálogo
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueToStart);
        }
        else
        {
            Debug.LogError("DialogueManager não encontrado na cena!");
        }
    }
}