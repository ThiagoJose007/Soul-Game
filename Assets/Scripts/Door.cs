using UnityEngine;

public class Door : MonoBehaviour
{
    // Função pública para ABRIR a porta
    public void OpenDoor()
    {
        Debug.Log("Porta foi aberta!");
        // A forma mais simples de "abrir": desativar o objeto.
        gameObject.SetActive(false); 
        
        // Outras ideias:
        // - Tocar um som de porta abrindo
        // - Iniciar uma animação
        // - Para um alçapão: transform.Rotate(0, 0, -90f);
    }

    // Função pública para FECHAR a porta
    public void CloseDoor()
    {
        Debug.Log("Porta foi fechada!");
        gameObject.SetActive(true);
    }
}
