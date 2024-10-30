using CthulhuGame;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEnabler : MonoBehaviour 
{
    [SerializeField] private GameObject[] _characters;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        DeactivateAllCharacters();

        _closeButton.onClick.AddListener(DeactivateAllCharacters);
        ActionButton.Instance.OnActionButtonClicked += ActivateRandomCharacter;
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(DeactivateAllCharacters);
        ActionButton.Instance.OnActionButtonClicked -= ActivateRandomCharacter;
    }

    private void ActivateRandomCharacter()
    {
        _characters[Random.Range(0, _characters.Length)].SetActive(true);
    }

    private void DeactivateAllCharacters()
    {
        foreach (var character in _characters)
        {
            character.SetActive(false);
        }
    }
}