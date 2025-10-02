using UnityEngine;
using System.Collections.Generic;

// A classe DialogueCharacter foi REMOVIDA.

[System.Serializable]
public class Dialogueline
{
    // Trocamos a referência ao DialogueCharacter por uma string simples.
    public string characterName;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<Dialogueline> dialoguelines = new List<Dialogueline>();
}