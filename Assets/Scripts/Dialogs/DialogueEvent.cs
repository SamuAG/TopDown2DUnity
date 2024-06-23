using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueEvent : MonoBehaviour
{
    protected Dialogue dialogueComponent;
    [SerializeField]
    protected GameManagerSO gM;

    [SerializeField] private List<StringArray> conditionalSentences;

    protected List<Action> events;

    protected virtual void Start()
    {
        dialogueComponent = GetComponent<Dialogue>();
        dialogueComponent.OnDialogueComplete += HandleDialogueCompletion;
        InitializeEvents();
    }

    protected void HandleDialogueCompletion(){
        int conditionIndex = CheckConditions();
        dialogueComponent.Sentences = conditionalSentences[conditionIndex].strings;
        events[conditionIndex]();
    }
    protected abstract int CheckConditions();

    protected abstract void InitializeEvents();
}

[Serializable]
public class StringArray
{
    public string[] strings;
}
