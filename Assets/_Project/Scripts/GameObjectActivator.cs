using CthulhuGame;
using UnityEngine;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private Character[] _characters;
    
    private void Start()
    {
        ActionButton.Instance.OnActionButtonClicked += ActivateCharacter;
    }

    private void OnDestroy()
    {
        ActionButton.Instance.OnActionButtonClicked -= ActivateCharacter;
    }

    private void ActivateCharacter()
    {
        foreach (Character character in _characters)
        {
            character.gameObject.SetActive(false);
        }

        int rnd = Random.Range(0, _characters.Length);

        _characters[rnd].gameObject.SetActive(true);
        _characters[rnd].SetDialoguePanelsActive(false);
        _characters[rnd].ShowRandomDialogue();
    }
}