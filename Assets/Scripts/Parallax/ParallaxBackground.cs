// Arquivo: ParallaxBackground.cs (Versão Atualizada)
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [Tooltip("Arraste o Transform do seu jogador aqui.")]
    public Transform playerTransform;
    
    private List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (playerTransform == null)
        {
            // Tenta encontrar o jogador pela tag "Player" se não for atribuído
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            if (playerTransform == null)
            {
                Debug.LogError("ParallaxBackground: Player Transform não atribuído e não encontrado com a tag 'Player'!");
                return;
            }
        }

        FindAndInitializeLayers();
    }

    void FindAndInitializeLayers()
    {
        parallaxLayers.Clear();
        // Percorre todos os filhos deste GameObject
        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                parallaxLayers.Add(layer);
                // Manda a referência do jogador para cada camada individualmente
                layer.Initialize(playerTransform);
            }
        }
    }

    // Funções públicas que o PlayerManager vai chamar
    public void ActivateSoulMode()
    {
        Debug.Log("2. ParallaxBackground: Ordem recebida! Comandando " + parallaxLayers.Count + " camadas.");
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.SwitchToSoulMode();
        }
    }

    public void DeactivateSoulMode()
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.SwitchToBodyMode();
        }
    }
}