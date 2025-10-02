using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private Transform interactionPoint; // Ponto de onde o raio sai
    [SerializeField] private float interactionDistance = 1.5f;
    [SerializeField] private LayerMask interactableLayer; // Filtro para só acertar objetos interativos

    // Esta é a função principal do componente.
    // Scripts externos (como o do jogador) irão chamá-la.
    public void CheckForInteraction(Vector2 direction)
    {
        Debug.Log("Interact");
        // Dispara o Raycast usando a direção fornecida pelo script do jogador
        RaycastHit2D hit = Physics2D.Raycast(
            interactionPoint.position, 
            direction, 
            interactionDistance, 
            interactableLayer
        );

        // Visualiza o raio no editor para facilitar o debug
        Debug.DrawRay(interactionPoint.position, direction * interactionDistance, Color.red);

        // Se acertou algo...
        if (hit.collider != null)
        {
            Debug.Log("Interactor atingiu: " + hit.collider.name);
            // ...tentamos pegar o componente "IInteractable"
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            
            // Se o componente existir...
            if (interactable != null)
            {
                // ...chamamos a sua função de interação!
                interactable.Interact();
            }
        }
    }
}
