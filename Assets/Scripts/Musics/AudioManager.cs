using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bodyMusicSource; // Arraste a "Musica_Corpo" aqui
    [SerializeField] private AudioSource soulMusicSource; // Arraste a "Musica_Alma" aqui

    [Header("Settings")]
    [Tooltip("Quanto tempo a transição de volume deve durar em segundos.")]
    [SerializeField] private float fadeDuration = 1.5f;

    private Coroutine crossfadeCoroutine;

    void Start()
    {
        // Garante que os volumes comecem corretamente
        bodyMusicSource.volume = 1f;
        soulMusicSource.volume = 0f;

        // Inicia as duas músicas ao mesmo tempo para que fiquem sincronizadas
        bodyMusicSource.Play();
        soulMusicSource.Play();
    }

    /// <summary>
    /// Inicia a transição suave para a música do Corpo.
    /// </summary>
    public void FadeToBodyMusic()
    {
        Debug.Log("Iniciando transição para a música do Corpo.");
        StartCrossfade(bodyMusicSource, soulMusicSource);
    }

    /// <summary>
    /// Inicia a transição suave para a música da Alma.
    /// </summary>
    public void FadeToSoulMusic()
    {
        Debug.Log("Iniciando transição para a música da Alma.");
        StartCrossfade(soulMusicSource, bodyMusicSource);
    }

    private void StartCrossfade(AudioSource fadeInSource, AudioSource fadeOutSource)
    {
        // Se já houver uma transição em andamento, pare-a
        if (crossfadeCoroutine != null)
        {
            StopCoroutine(crossfadeCoroutine);
        }
        // Inicia a nova transição
        crossfadeCoroutine = StartCoroutine(DoCrossfade(fadeInSource, fadeOutSource));
    }

    private IEnumerator DoCrossfade(AudioSource fadeInSource, AudioSource fadeOutSource)
    {
        float timer = 0f;
        // Pega os volumes iniciais
        float startFadeInVolume = fadeInSource.volume;
        float startFadeOutVolume = fadeOutSource.volume;

        while (timer < fadeDuration)
        {
            // Calcula o progresso da transição (de 0 a 1)
            float progress = timer / fadeDuration;

            // Altera o volume de forma linear
            fadeInSource.volume = Mathf.Lerp(startFadeInVolume, 1f, progress); // Sobe para 100%
            fadeOutSource.volume = Mathf.Lerp(startFadeOutVolume, 0f, progress); // Desce para 0%

            // Avança o tempo e espera o próximo frame
            timer += Time.deltaTime;
            yield return null;
        }

        // Garante que os volumes finais sejam exatos
        fadeInSource.volume = 1f;
        fadeOutSource.volume = 0f;
    }
    
}