// Arquivo: ImageSlideshowManager.cs
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageSlideshowManager : MonoBehaviour
{
    public static ImageSlideshowManager Instance;

    [Header("UI Reference")]
    [SerializeField] private Image slideshowImageUI; // A Imagem que vai exibir os slides
    [SerializeField] private GameObject slideshowCanvas; // O Canvas pai para ligar/desligar

    private void Awake()
    {
        // Padrão Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Inicia a sequência de imagens.
    /// </summary>
    public void StartSlideshow(ImageSequenceData sequence)
    {
        // Garante que o Canvas e a Imagem estejam prontos
        if (slideshowCanvas == null || slideshowImageUI == null)
        {
            Debug.LogError("Setup da UI do Slideshow está incompleto no Inspector!");
            return;
        }
        
        slideshowCanvas.SetActive(true);
        StartCoroutine(PlaySlideshow(sequence));
    }

    private IEnumerator PlaySlideshow(ImageSequenceData sequence)
    {
        Debug.Log("Iniciando slideshow...");
        
        // Esconde a imagem no início para evitar um "flash" da imagem anterior
        slideshowImageUI.gameObject.SetActive(false); 
        yield return null; // Espera um frame

        // Passa por cada "frame" (imagem + duração) da sequência
        foreach (SlideshowFrame frame in sequence.frames)
        {
            if (frame.image != null)
            {
                // Mostra a imagem
                slideshowImageUI.sprite = frame.image;
                slideshowImageUI.gameObject.SetActive(true);

                // Espera pela duração definida
                yield return new WaitForSeconds(frame.duration);
                
                // Esconde a imagem para a transição
                slideshowImageUI.gameObject.SetActive(false);
            }
        }

        // --- FINALIZA TUDO ---
        Debug.Log("Slideshow finalizado. Carregando cena: " + sequence.nextSceneToLoad);
        slideshowCanvas.SetActive(false); // Esconde o Canvas antes de trocar de cena

        if (!string.IsNullOrEmpty(sequence.nextSceneToLoad))
        {
            SceneManager.LoadScene(sequence.nextSceneToLoad);
        }
    }
}