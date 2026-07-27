using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIUpgrade : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button chooseButton;

        public void Initialize(Sprite icon, string title, string description, Action choose)
        {
            image.sprite = icon;
            titleText.text = title;
            descriptionText.text = description;
            chooseButton.gameObject.SetActive(true);
            chooseButton.onClick.RemoveAllListeners();
            chooseButton.onClick.AddListener(() => choose?.Invoke());
        }

        public void SetViewOnly()
        {
            chooseButton.gameObject.SetActive(false);
        }
    }
}