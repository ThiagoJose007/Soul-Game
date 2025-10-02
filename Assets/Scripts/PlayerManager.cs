using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    // Arraste os GameObjects do Corpo e da Alma aqui no Inspector
    public GameObject corpo;
    public GameObject alma;
    public Camera mainCamera; 
    public List<SoulObject> objetosDeAlma;
    public Animator animator;
    
    public SpriteRenderer SoulSpriteRenderer; 
    public Color corNormalAlma = Color.white;
    public Color corPodeVoltar;
    
    // Referências para os scripts de movimento
    private MovimentoCorpo movimentoCorpo;
    private MovimentoSoul movimentoAlma;
    private bool SoulCanReturn = false;

    [Header("World Control")]
    public ParallaxBackground parallaxBackground;
    
    // Variável para controlar o estado
    private bool controlandoAlma = false;
    [Header("Audio")]
    public AudioManager audioManager; // <-- NOVA REFERÊNCIA
    private bool isSwapping = false; // Previne trocas rápidas enquanto a animação toca
    void Start()
    {
        // Pega os componentes dos scripts
        movimentoCorpo = corpo.GetComponent<MovimentoCorpo>();
        movimentoAlma = alma.GetComponent<MovimentoSoul>();

        // Começa o jogo controlando o corpo
        alma.SetActive(false);
        movimentoCorpo.enabled = true;
        movimentoAlma.enabled = false;
    }

    void Update()
    {
        // Verifica se o botão foi pressionado (Ex: tecla Espaço)
        if (Input.GetButtonDown("Soul"))
        {
            TrocarControle();
        }
    }

    
    public void DefinirPodeVoltar(bool status)
    {
        SoulCanReturn = status;
        
        // --- FEEDBACK VISUAL (MUITO RECOMENDADO!) ---
        if (SoulSpriteRenderer != null)
        {
            // Se pode voltar, muda a cor da alma para verde (ou outra cor)
            // Se não pode, volta para a cor normal
            SoulSpriteRenderer.color = status ? corPodeVoltar : corNormalAlma;
        }
    }
    void TrocarControle()
    {
        // PRIMEIRO, VERIFICAMOS O ESTADO ATUAL

        // --- CASO 1: Estamos controlando a ALMA e queremos voltar para o CORPO ---
        if (controlandoAlma)
        {
            // A CONDIÇÃO É VERIFICADA AQUI DENTRO!
            if (SoulCanReturn)
            {
                // Se a condição for válida, executa a troca de volta para o Corpo.
                movimentoCorpo.enabled = true;
                movimentoAlma.enabled = false;
                alma.SetActive(false);
                
                if (audioManager != null)
                {
                    audioManager.FadeToBodyMusic();
                }
                // Desliga o mundo da alma
                mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer("SoulOnly"));
                foreach (SoulObject obj in objetosDeAlma)
                {
                    obj.Deactivate();
                }

                if (SoulSpriteRenderer != null)
                {
                    SoulSpriteRenderer.color = corNormalAlma;
                }
                Debug.Log("1. PlayerManager: Ordem para DESATIVAR MODO ALMA enviada!");
                if (parallaxBackground != null) parallaxBackground.DeactivateSoulMode();
                animator.SetBool("IsSoul", false);
                // E SÓ NO FINAL, atualizamos o estado.
                controlandoAlma = false;
            }
            else
            {
                // Se não puder voltar, a função simplesmente não faz nada.
                Debug.Log("Não posso voltar ao corpo aqui!");
                // (Você pode adicionar um som de "falha" aqui)
                return; // Sai da função sem fazer a troca.
                // DESLIGA a área de retorno
                
            }
        }
        // --- CASO 2: Estamos controlando o CORPO e queremos virar ALMA ---
        else // (Isso significa que !controlandoAlma é verdadeiro)
        {
            if (audioManager != null)
            {
                audioManager.FadeToSoulMusic();
            }
            // A troca para a Alma não tem condição, sempre pode acontecer.
            alma.transform.position = corpo.transform.position;
            alma.SetActive(true);
            movimentoCorpo.enabled = false;
            movimentoAlma.enabled = true;
            corpo.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            animator.SetBool("IsSoul", true);
            // Liga o mundo da alma
            mainCamera.cullingMask |= 1 << LayerMask.NameToLayer("SoulOnly");
            foreach (SoulObject obj in objetosDeAlma)
            {
                obj.Activate();
            }
            Debug.Log("1. PlayerManager: Ordem para ATIVAR MODO ALMA enviada!");
            if (parallaxBackground != null) parallaxBackground.ActivateSoulMode();
            // E SÓ NO FINAL, atualizamos o estado.
            controlandoAlma = true;
        }
    }
}
