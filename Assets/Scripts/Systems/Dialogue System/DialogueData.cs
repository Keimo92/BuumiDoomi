using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string[] dialogueLines;

    public bool autoSkip;
}
