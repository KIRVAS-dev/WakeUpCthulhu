using UnityEngine;
using UnityEngine.UI;

namespace CthulhuGame
{
    public class FishCard : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _name;
        [SerializeField] private Text _description;
        [SerializeField] private Text _weight;
        [SerializeField] private Text _cost;

        public void Initialize()
        {
            var fish = Player.Instance.FishingRod.CaughtFish;      
            var sprite = fish.Sprite;

            _image.sprite = sprite.sprite;
            _name.text = fish.Name;
            _cost.text = $"{fish.Cost} �����";
            _weight.text = $"{fish.Weight} ��";
            _description.text = fish.Description;
        }
    }
}