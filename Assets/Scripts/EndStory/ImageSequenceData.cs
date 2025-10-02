using UnityEngine;
using System.Collections.Generic;

// Um "pacote" que guarda uma imagem e sua duração na tela.
[System.Serializable]
public class SlideshowFrame
{
    public Sprite image;
    public float duration = 2f; // Duração padrão de 2 segundos
}

// Um "pacote" que representa a sequência completa.
[System.Serializable]
public class ImageSequenceData
{
    [Tooltip("A lista de imagens e suas durações, em ordem.")]
    public List<SlideshowFrame> frames = new List<SlideshowFrame>();

    [Tooltip("A cena a ser carregada após a última imagem.")]
    public string nextSceneToLoad;
}
