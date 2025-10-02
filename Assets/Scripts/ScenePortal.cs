// Arquivo: ScenePortal.cs
using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para carregar cenas

public class ScenePortal : MonoBehaviour
{
    [Header("Portal Settings")]
    [Tooltip("O nome EXATO do arquivo da cena de destino.")]
    [SerializeField] private string destinationSceneName;

    [Tooltip("Selecione a Layer que representa o corpo físico do jogador.")]
    [SerializeField] private LayerMask playerBodyLayer;
    
    // Esta função é chamada automaticamente quando um objeto com Rigidbody2D entra no trigger.
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Verificamos se a layer do objeto que entrou é a layer do corpo do jogador.
        // Este código bitwise é a forma mais correta de checar uma LayerMask.
        if ((playerBodyLayer.value & (1 << otherCollider.gameObject.layer)) > 0)
        {
            Debug.Log("Jogador entrou no portal! Carregando cena: " + destinationSceneName);
            LoadDestinationScene();
        }
    }

    private void LoadDestinationScene()
    {
        // Verifica se o nome da cena foi definido antes de tentar carregar
        if (!string.IsNullOrEmpty(destinationSceneName))
        {
            SceneManager.LoadScene(destinationSceneName);
        }
        else
        {
            Debug.LogError("O nome da cena de destino não foi definido no Inspector deste portal!", this.gameObject);
        }
    }
}