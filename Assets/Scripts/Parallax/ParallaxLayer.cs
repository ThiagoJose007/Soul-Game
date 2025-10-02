// Arquivo: ParallaxLayer.cs (Versão Atualizada com Sprites)
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Header("Faux Parallax Settings")]
    [Tooltip("Força do efeito. Use valores entre -0.1 e -0.5 para camadas próximas. Quanto mais próximo, maior o valor absoluto. Ex: -0.1 (fundo), -0.3 (meio), -0.5 (frente).")]
    public float parallaxFactor;
    
    [Tooltip("A posição original de onde esta camada começa. Calculado automaticamente.")]
    private Vector3 originalLocalPosition;

    [Header("Soul Mode Sprites")]
    [Tooltip("O Sprite padrão (Corpo) para esta camada.")]
    public Sprite bodyModeSprite;
    [Tooltip("O Sprite (Alma) para esta camada.")]
    public Sprite soulModeSprite;

    private Transform playerTransform; // Referência ao Transform do jogador
    private SpriteRenderer spriteRenderer;

    // Método chamado pelo ParallaxBackground para inicializar a camada
    public void Initialize(Transform player)
    {
        playerTransform = player;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalLocalPosition = transform.localPosition; // Usamos localPosition para evitar problemas com o pai

        // Garante que o sprite inicial seja o do corpo
        if (bodyModeSprite == null && spriteRenderer.sprite != null)
        {
            bodyModeSprite = spriteRenderer.sprite; // Se não definido, pega o atual
        }
        else if (bodyModeSprite != null)
        {
            spriteRenderer.sprite = bodyModeSprite; // Garante que comece com o sprite do corpo
        }
    }

    void LateUpdate()
    {
        // Garante que o parallax só funcione se o jogador existir
        if (playerTransform != null)
        {
            // Calcula o deslocamento. Quanto mais longe do centro X (0), mais o fundo se move.
            // O sinal negativo inverte o movimento, criando a ilusão de parallax.
            // Dividimos pela largura da tela ou tamanho da área visível para normalizar.
            float cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect; // Largura visível
            float playerNormalizedX = (playerTransform.position.x / cameraWidth) * 2; // Normaliza de -1 a 1

            // Aplica o deslocamento horizontal
            Vector3 newPosition = originalLocalPosition;
            newPosition.x += playerNormalizedX * parallaxFactor;
            
            transform.localPosition = newPosition;
        }
    }

    // Funções de comando para o modo alma (trocam o sprite)
    public void SwitchToSoulMode()
    {
        Debug.Log("3. Camada '" + gameObject.name + "': Ordem recebida para trocar sprite.");
        if (spriteRenderer != null && soulModeSprite != null)
        {
            spriteRenderer.sprite = soulModeSprite;
        }
    }

    public void SwitchToBodyMode()
    {
        if (spriteRenderer != null && bodyModeSprite != null)
        {
            spriteRenderer.sprite = bodyModeSprite;
        }
    }
}