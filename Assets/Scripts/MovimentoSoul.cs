using UnityEngine;

public class MovimentoSoul : MonoBehaviour
{
    public float velocidade = 4f;
    private Rigidbody2D rb;
    private Vector2 direcao;
    private bool isFacingRight = true;
    private Vector2 lookDirection = Vector2.right;
    // --- NOVA REFERÊNCIA para o nosso componente ---
    private Interactor interactor;
    private Animator animator;
    public float movementSmoothing = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Importante: garantir que a alma não seja afetada pela gravidade
        rb.gravityScale = 0;
        interactor = GetComponent<Interactor>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Pega o input do jogador (horizontal e vertical)
        direcao.x = Input.GetAxisRaw("Horizontal");
        direcao.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("speed", direcao.magnitude);
        
        if (direcao.x < 0f && isFacingRight)
        {
            Flip();
        }
        else if (direcao.x > 0f && !isFacingRight)
        {
            Flip();
        }
        // --- LÓGICA DE INTERAÇÃO REUTILIZADA ---
        if (Input.GetButtonDown("Interact"))
        {
            if (interactor != null)
            {
                interactor.CheckForInteraction(lookDirection);
            }
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }
    void FixedUpdate()
    {
        // 1. Define a velocidade ALVO baseada no seu input
        Vector2 targetVelocity = direcao.normalized * velocidade;

        // 2. Interpola suavemente da velocidade ATUAL (rb.velocity) para a velocidade ALVO
        // Time.fixedDeltaTime garante que a suavização seja consistente e independente do framerate.
        // movementSmoothing é o nosso "botão" para controlar a rapidez da transição.
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, Time.fixedDeltaTime * movementSmoothing);
    }
}
