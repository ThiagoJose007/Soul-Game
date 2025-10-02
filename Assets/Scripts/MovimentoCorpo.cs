using UnityEngine;

public class MovimentoCorpo : MonoBehaviour
{
    public float velocidade = 5f;
    [Tooltip("Controla a aceleração/desaceleração. Valores mais altos = mais rápido.")]
    public float movementSmoothing = 10f;
    private Rigidbody2D rb;
    private float direcaoX;
    private bool isFacingRight = true; // <-- NOVA VARIÁVEL

    private Vector2 lookDirection = Vector2.right;
    
    // --- NOVAS Variáveis para o Pulo ---
    [Header("Configurações do Pulo")]
    public float JumpForce = 10f;
    public Transform PlayerFoot; // Um objeto vazio posicionado nos pés do personagem
    public float CheckGround = 0.2f; // O raio do círculo que vamos "carimbar"
    public LayerMask WhatGround; // Um filtro para dizer o que é considerado chão
    private bool InGround;
    
    // --- NOVA REFERÊNCIA para o nosso componente ---
    private Interactor interactor;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Pega o componente Interactor que está no mesmo GameObject
        interactor = GetComponent<Interactor>();
        animator = GetComponent<Animator>(); // <-- Pega o componente Animator
        // --- NOVA LÓGICA DE SPAWN ---
            // Procura por um GameObject na cena que tenha a tag "SpawnPoint"
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        
            // Se um objeto com essa tag foi encontrado...
            if (spawnPoint != null)
            {
                // ...move o jogador para a posição exata daquele objeto.
                transform.position = spawnPoint.transform.position;
                Debug.Log("Jogador movido para o SpawnPoint.");
            }
            else
            {
                // Se não encontrou, avisa no console. O jogador começará onde foi deixado no editor.
                Debug.LogWarning("Nenhum 'SpawnPoint' encontrado na cena. O jogador começará na sua posição inicial.");
            }
    }

    void Update()
    {
        // Pega o input do jogador (esquerda/direita)
        direcaoX = Input.GetAxisRaw("Horizontal");
        // --- LÓGICA DA VERIFICAÇÃO DO CHÃO (Physics) ---
        // A mágica acontece aqui: criamos um círculo na posição 'peDoJogador' e vemos se ele colide com a layer 'oQueEChao'
        InGround = Physics2D.OverlapCircle(PlayerFoot.position, CheckGround, WhatGround);
        // --- ATUALIZA OS PARÂMETROS DO ANIMATOR ---
        // Usamos a velocidade real do Rigidbody para mais precisão. Mathf.Abs para ser sempre positivo.
        animator.SetFloat("speed", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isGrounded", InGround);
        // --- NOVA LÓGICA PARA VIRAR O PERSONAGEM E SEUS FILHOS ---
        // Se estamos indo para a esquerda e ainda estamos virados para a direita...
        if (direcaoX < 0f && isFacingRight)
        {
            Flip(); // ...vire!
        }
        // Se estamos indo para a direita e estamos virados para a esquerda...
        else if (direcaoX > 0f && !isFacingRight)
        {
            Flip(); // ...vire!
        }
        if (Input.GetButtonDown("Jump") && InGround)
        {
            
            // Aplicamos a força do pulo de uma vez só
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
            animator.SetTrigger("jump"); // A linha que deveria tocar a animação
        }
        // --- LÓGICA DE INTERAÇÃO SIMPLIFICADA ---
        if (Input.GetButtonDown("Interact"))
        {
            
            // Se o componente existir, chame a função dele, passando a direção.
            if (interactor != null)
            {
                interactor.CheckForInteraction(lookDirection);
            }
        }
        
    }
    private void Flip()
    {
        // Inverte o estado da direção
        isFacingRight = !isFacingRight;

        // Pega a escala local atual
        Vector3 localScale = transform.localScale;
        // Inverte o eixo X
        localScale.x *= -1f;
        // Aplica a nova escala de volta ao transform
        transform.localScale = localScale;
    }

    // --- DICA PRO: Visualizar o CircleCast no Editor ---
    // Este método desenha uma forma na sua tela de Scene para você ver o raio do chão
    private void OnDrawGizmosSelected()
    {
        if (PlayerFoot == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(PlayerFoot.position, CheckGround);
    }

    void FixedUpdate()
    {
        // --- LÓGICA DE MOVIMENTO ATUALIZADA ---
        // 1. Define a velocidade ALVO baseada no seu input
        float targetVelocity = direcaoX * velocidade;

        // 2. Interpola suavemente da velocidade ATUAL (rb.velocity) para a velocidade ALVO
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, new Vector2(targetVelocity, rb.linearVelocity.y), Time.fixedDeltaTime * movementSmoothing);
    }
}