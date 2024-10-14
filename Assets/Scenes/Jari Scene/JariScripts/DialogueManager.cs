using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;
    public float clearTextDelay = 2.0f; 

    private Queue<string> dialogueQueue;
    private bool isHealthPackInfo = false; 

    private void Start()
    {
        dialogueQueue = new Queue<string>();
        dialogueText.text = "";
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        isHealthPackInfo = false;
        dialogueQueue.Clear();

        foreach (string line in dialogueData.dialogueLines)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void StartHealthPackInfo(HealthPack health)
    {
        isHealthPackInfo = true;
        dialogueQueue.Clear();

        foreach (string line in health.HealthPackInfo)
        {
            dialogueQueue.Enqueue(line);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string line = dialogueQueue.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (isHealthPackInfo)
        {
            yield return new WaitForSeconds(1.0f);
            DisplayNextLine();
        }
        else
        {
            yield return StartCoroutine(WaitForEnterKey());
            DisplayNextLine();
        }
    }

    IEnumerator WaitForEnterKey()
    {
        while (!Input.GetMouseButton(1))
        {
            yield return null;
        }
    }

    public void EndDialogue()
    {
        StartCoroutine(ClearDialogueTextAfterDelay(clearTextDelay));
    }

    IEnumerator ClearDialogueTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueText.text = "";
    }
}
