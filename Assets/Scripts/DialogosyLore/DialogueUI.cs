using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private ObjetoDialogo testDialogue;
    [SerializeField] private GameObject dialoguePanel;
    public float tiempoEntreDialogos = 1f;

    private TypewriterEffect typewriterEffect;
    
    private void Start() {
        typewriterEffect = GetComponent<TypewriterEffect>();
        CerrarDialogo();
        ShowDialogue(testDialogue);
    }
    public void ShowDialogue(ObjetoDialogo dialogueObject) {
        dialoguePanel.SetActive(true);
        StartCoroutine( routine: StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(ObjetoDialogo dialogueObject) {
        foreach (string dialogue in dialogueObject.BurbujasDialogo) {
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
        }
        CerrarDialogo();
    }

    private void CerrarDialogo() {
        dialoguePanel.SetActive(false);
        textLabel.text = string.Empty;
    }
}
