using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject[] _dialoguePanels;

    public void SetDialoguePanelsActive(bool value)
    {
        if (_dialoguePanels.Length > 0)
        {
            foreach (GameObject panel in _dialoguePanels)
            {
                panel.SetActive(value);
            }
        }
    }

    public void ShowRandomDialogue()
    {
        _dialoguePanels[Random.Range(0, _dialoguePanels.Length)].SetActive(true);
    }
}