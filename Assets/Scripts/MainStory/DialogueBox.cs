// Arquivo: DialogueBox.cs
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    // --- NOVA REFERÊNCIA ---
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI speakerNameText; // Arraste o texto do nome aqui (ex: CatName)
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Settings")]
    [SerializeField] private float typingSpeed = 0.04f;
    
    public bool isTyping { get; private set; } = false;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        Hide();
    }

    // --- ASSINATURA DO MÉTODO ATUALIZADA ---
    // Agora ele aceita o nome do falante e o texto
    public void StartTyping(string speakerName, string textToDisplay)
    {
        Show();
        
        // --- NOVA LINHA ---
        // Atualiza o campo de texto do nome
        if (speakerNameText != null)
        {
            speakerNameText.text = speakerName;
        }

        typingCoroutine = StartCoroutine(TypeSentence(textToDisplay));
    }

    public void SkipTyping(string fullText)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = fullText;
            isTyping = false;
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }
    
    public void Hide()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }
    
    private void Show()
    {
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
    }
}