// Arquivo: SlideshowTrigger.cs
using UnityEngine;

public class SlideshowTrigger : MonoBehaviour
{
    [Header("Slideshow Data")]
    [Tooltip("A sequência de imagens que este gatilho deve iniciar.")]
    public ImageSequenceData sequence;

    [Tooltip("Marque para iniciar automaticamente quando a cena carregar.")]
    public bool triggerOnSceneStart = true;

    void Start()
    {
        if (triggerOnSceneStart)
        {
            StartTheSlideshow();
        }
    }

    // Você pode chamar esta função a partir de outros eventos também (um botão, um portal, etc.)
    public void StartTheSlideshow()
    {
        if (ImageSlideshowManager.Instance != null)
        {
            ImageSlideshowManager.Instance.StartSlideshow(sequence);
        }
        else
        {
            Debug.LogError("ImageSlideshowManager não encontrado na cena!");
        }
    }
}