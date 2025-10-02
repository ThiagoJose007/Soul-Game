using UnityEngine;

public class ReturnArea : MonoBehaviour
{
    // Arraste seu PlayerManager aqui pelo Inspector
    public PlayerManager playerManager;

    // NOSSO NOVO INTERRUPTOR. Começa desligado.
    private bool isAreaActive = false;

    // NOVA FUNÇÃO PÚBLICA que o PlayerManager vai chamar
    public void SetAreaActive(bool status)
    {
        isAreaActive = status;
        Debug.Log("Área de Retorno agora está: " + (status ? "ATIVA" : "INATIVA"));
    }
    // Função que é chamada automaticamente quando um colisor ENTRA no trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTROU: " + other.name);
        // Verificamos se o objeto que entrou tem a tag "Alma"
        if (other.CompareTag("Soul"))
        {
            // Se for a alma, avisamos o PlayerManager que agora PODE voltar
            playerManager.DefinirPodeVoltar(true);
        }
    }

    // Função que é chamada automaticamente quando um colisor SAI do trigger
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("SAIU: " + other.name);   
        // Verificamos se o objeto que saiu tem a tag "Alma"
        if (other.CompareTag("Soul"))
        {
            // Se for a alma, avisamos o PlayerManager que agora NÃO PODE mais voltar
            playerManager.DefinirPodeVoltar(false);
        }
    }
}
