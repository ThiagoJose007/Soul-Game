using UnityEngine;

public class GrabObjects : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint; // Ponto para onde o objeto é movido ao ser pego

    [SerializeField]
    private Transform rayPoint; // Ponto de onde o Raycast é disparado

    [SerializeField]
    private float rayDistance; // Distância do Raycast

    private GameObject grabbedObject; // Objeto que está sendo segurado
    public LayerMask layerIndex; // O índice da layer dos objetos pegáveis
    private Animator animator;
    private bool isGrabbing = false;
    private void Awake()
    {
        // Pega o componente Animator no início
        animator = GetComponent<Animator>();
    }
    private void Update()
    { // Se o Raycast acertou um colisor na layer correta...
        if (Input.GetButton("Grab") && grabbedObject == null) 
        {   
            
            Vector2 rayDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            // Dispara um Raycast a partir do rayPoint
            RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, transform.right, rayDistance, layerIndex);
            // E se apertamos Espaço e NÃO estamos segurando nada...
            if (hitInfo.collider != null)
            {
                isGrabbing = true;
                animator.SetBool("isGrabbing", true);
                // --- LÓGICA DE PEGAR O OBJETO ---
                grabbedObject = hitInfo.collider.gameObject;
                // Torna o objeto Kinematic (ignora a física)
                grabbedObject.GetComponent<Rigidbody2D>().simulated = false;
                grabbedObject.GetComponent<Collider2D>().isTrigger = true;
                // Move o objeto para o grabPoint
                grabbedObject.transform.position = grabPoint.position;
                // Torna o objeto um "filho" do jogador, para que se mova junto
                grabbedObject.transform.SetParent(transform);
                
            }
        }

        // Se apertamos Espaço e JÁ ESTAMOS segurando um objeto...
        if (Input.GetButtonUp("Grab") && grabbedObject != null)
        {
            isGrabbing = false;
            animator.SetBool("isGrabbing", false);
            // --- LÓGICA DE SOLTAR O OBJETO ---
            // Devolve a física ao objeto
            grabbedObject.GetComponent<Rigidbody2D>().simulated = true;
            grabbedObject.GetComponent<Collider2D>().isTrigger = false;
            // O objeto deixa de ser "filho" do jogador
            grabbedObject.transform.SetParent(null);
            // "Esquecemos" o objeto que estávamos segurando
            grabbedObject = null;
        }

        // A lógica de soltar nas imagens está um pouco confusa (dentro de um "else"). 
        // Esta versão separada é mais clara e funcional.
    }
}
