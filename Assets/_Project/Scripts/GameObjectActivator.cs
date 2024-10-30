using CthulhuGame;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectActivator : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObjects;
    [SerializeField] private Button _closeButton;

    private void Start()
    {
        DeactivateAllGameObjects();

        _closeButton.onClick.AddListener(DeactivateAllGameObjects);
        ActionButton.Instance.OnActionButtonClicked += ActivateRandomGameObject;
    }

    private void OnDestroy()
    {
        _closeButton.onClick.RemoveListener(DeactivateAllGameObjects);
        ActionButton.Instance.OnActionButtonClicked -= ActivateRandomGameObject;
    }

    private void ActivateRandomGameObject()
    {
        _gameObjects[Random.Range(0, _gameObjects.Length)].SetActive(true);
    }

    private void DeactivateAllGameObjects()
    {
        foreach (var gameObject in _gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}