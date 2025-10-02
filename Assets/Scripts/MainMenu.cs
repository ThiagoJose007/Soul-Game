// Arquivo: MainMenu.cs
using UnityEngine;
using UnityEngine.SceneManagement; // Essencial para gerenciar as cenas!

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Esta função será chamada pelo botão "Play".
    /// Ela carrega a cena cujo nome é passado como parâmetro.
    /// </summary>
    /// <param name="sceneName">O nome EXATO do arquivo da cena que você quer carregar.</param>
    public void PlayGame(string sceneName)
    {
        Debug.Log("Carregando cena: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Esta função será chamada pelo botão "Exit".
    /// Ela fecha a aplicação.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");

        // Esta linha de código fecha o jogo.
        // IMPORTANTE: Application.Quit() só funciona no jogo compilado (.exe, .apk, etc.).
        // Não funciona no Editor do Unity.
        Application.Quit();

        // Esta parte abaixo é um truque para fazer o jogo "parar" no editor também.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}