// Arquivo: DialogueManager.cs (versão simplificada)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    // --- ADICIONE ESTA LINHA ---
    [Header("Managers")]
    [SerializeField] private AudioManager audioManager;
    // --- NOVAS VARIÁVEIS PARA A SEQUÊNCIA DE FUNDO ---
    [Header("Background Sequence")]
    [Tooltip("O componente Image que será usado para o fundo da cutscene.")]
    
    [SerializeField] private Image mainBackgroundImage;
    [Tooltip("A imagem que ficará visível DURANTE o diálogo.")]
    [SerializeField] private Sprite dialogueBackground;
    [Tooltip("A primeira imagem que aparecerá APÓS o diálogo.")]
    [SerializeField] private Sprite postDialogueImage1;
    [Tooltip("Duração da primeira imagem pós-diálogo.")]
    [SerializeField] private float postDialogueImage1Duration = 2f;
    [Tooltip("A segunda imagem que aparecerá APÓS o diálogo.")]
    [SerializeField] private Sprite postDialogueImage2;
    [Tooltip("Duração da segunda imagem pós-diálogo.")]
    [SerializeField] private float postDialogueImage2Duration = 2f;
    
    [Header("UI Boxes")]
    [Tooltip("Arraste o GameObject da caixa de diálogo do Gato.")]
    [SerializeField] private DialogueBox catDialogueBox;
    [Tooltip("Arraste o GameObject da caixa de diálogo do Criador.")]
    [SerializeField] private DialogueBox creatorDialogueBox;

    [SerializeField] private string nextSceneToLoad;
    
    // Nomes dos personagens para evitar erros de digitação.
    // Certifique-se de que o nome que você digita no Inspector é EXATAMENTE igual a estes.
    private const string CAT_NAME = "Gato";
    private const string CREATOR_NAME = "Criador";

    private Queue<Dialogueline> lines;
    private bool isDialogueActive = false;
    private Dialogueline currentLine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            lines = new Queue<Dialogueline>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isDialogueActive && Input.GetButtonDown("Interact"))
        {
            bool isCatTyping = catDialogueBox.isTyping;
            bool isCreatorTyping = creatorDialogueBox.isTyping;

            if (isCatTyping)
            {
                catDialogueBox.SkipTyping(currentLine.line);
            }
            else if (isCreatorTyping)
            {
                creatorDialogueBox.SkipTyping(currentLine.line);
            }
            else
            {
                DisplayNextDialogueLine();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // --- LÓGICA ATUALIZADA ---
        // Mostra a imagem de fundo principal no início
        if (mainBackgroundImage != null && dialogueBackground != null)
        {
            mainBackgroundImage.sprite = dialogueBackground;
            mainBackgroundImage.gameObject.SetActive(true);
        }

        isDialogueActive = true;
        catDialogueBox.Hide();
        creatorDialogueBox.Hide();

        lines.Clear();
        foreach (Dialogueline dialogueLine in dialogue.dialoguelines)
        {
            lines.Enqueue(dialogueLine);
        }
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();

        DialogueBox activeBox = null;
        if (currentLine.characterName == "Gato") // Usando a constante seria melhor, mas isso funciona
        {
            activeBox = catDialogueBox;
            creatorDialogueBox.Hide();
        }
        else if (currentLine.characterName == "Criador")
        {
            activeBox = creatorDialogueBox;
            catDialogueBox.Hide();
        }

        if (activeBox != null)
        {
            // --- LINHA ATUALIZADA ---
            // Antes: activeBox.StartTyping(currentLine.line);
            // Agora, passamos o nome E a fala.
            activeBox.StartTyping(currentLine.characterName, currentLine.line);
        }
        else
        {
            Debug.LogWarning("Nome de personagem não reconhecido: " + currentLine.characterName);
            DisplayNextDialogueLine();
        }
    }

    private IEnumerator PostDialogueSequence()
    {
        // --- MOSTRA IMAGEM 1 ---
        if (mainBackgroundImage != null && postDialogueImage1 != null)
        {
            mainBackgroundImage.sprite = postDialogueImage1;
            // Espera pela duração definida
            yield return new WaitForSeconds(postDialogueImage1Duration);
        }

        // --- MOSTRA IMAGEM 2 ---
        if (mainBackgroundImage != null && postDialogueImage2 != null)
        {
            mainBackgroundImage.sprite = postDialogueImage2;
            // Espera pela duração definida
            yield return new WaitForSeconds(postDialogueImage2Duration);
        }

        // --- FINALIZA TUDO ---
        Debug.Log("Sequência finalizada. Carregando próxima cena.");
    
        // (Opcional: fade out da tela aqui)

        // Esconde a imagem de fundo antes de carregar a cena
        if (mainBackgroundImage != null)
        {
            mainBackgroundImage.gameObject.SetActive(false);
        }
    
        // Carrega a próxima cena (se houver uma definida)
        if (!string.IsNullOrEmpty(nextSceneToLoad))
        {
            SceneManager.LoadScene(nextSceneToLoad);
        }
    }
    private void EndDialogue()
    {
        isDialogueActive = false;
        catDialogueBox.Hide();
        creatorDialogueBox.Hide();
        Debug.Log("Diálogo finalizado. Iniciando sequência de imagens finais...");

        // Em vez de terminar, inicia a próxima fase da cutscene
        StartCoroutine(PostDialogueSequence());
    }
}