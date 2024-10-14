using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueData Data;
    
    
    DialogueManager DialogueManager;



    private void Start()
    {
        DialogueManager = FindAnyObjectByType<DialogueManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.TryGetComponent<Entity>(out Entity entity ))
        {
            DialogueManager.StartDialogue(Data);
            this.gameObject.SetActive(false);

        }
    }
}
