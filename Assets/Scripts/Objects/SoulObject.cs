using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class SoulObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteRenderer spriteRenderer; // Opcional: para o visual

    void Awake()
    {
        // Pega os componentes no início
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Começa desativado por padrão
        Deactivate();
    }

    // Função para tornar o objeto visível e tangível
    public void Activate()
    {
        // Ativa o colisor para que o jogador possa interagir
        col.enabled = true;
        
        // Retorna o Rigidbody para o seu estado normal (Dynamic)
        rb.bodyType = RigidbodyType2D.Dynamic;
        
        // Opcional: Torna o objeto visível
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    // Função para tornar o objeto invisível e intangível
    public void Deactivate()
    {
        // Desativa o colisor para que o jogador atravesse
        col.enabled = false;
        
        // Mude para Kinematic para "congelar" o objeto no lugar e ignorar a gravidade
        rb.bodyType = RigidbodyType2D.Kinematic;
        // Zera a velocidade para garantir que ele pare completamente
        rb.linearVelocity = Vector2.zero;
        
        // Opcional: Torna o objeto invisível
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
    }
}