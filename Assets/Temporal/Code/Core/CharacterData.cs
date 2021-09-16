using System;
using Doozy.Engine;
using Doozy.Engine.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Code.Core
{
    public class CharacterData : MonoBehaviour
    {
        public string popupName = "UIPopup - CharacterCard";

        /// <summary> Reference to the UIPopup clone used by this script</summary>
        private UIPopup _popup;
        [FoldoutGroup("Data"), ColorBox] public string name;
        [FoldoutGroup("Data"), ColorBox] public string powerValue;
        [FoldoutGroup("Data"), ColorBox] public string healthValue;
        [FoldoutGroup("Data"), ColorBox] public string damageValue;
        [FoldoutGroup("Data"), ColorBox] public string ballsValue;
        [FoldoutGroup("Data"), ColorBox] public Sprite portrait;

        [FoldoutGroup("Card"), ColorBox] public TMP_Text nameText;
        [FoldoutGroup("Card"), ColorBox] public TMP_Text powerText;
        [FoldoutGroup("Card"), ColorBox] public TMP_Text healthText;
        [FoldoutGroup("Card"), ColorBox] public TMP_Text damageText;
        [FoldoutGroup("Card"), ColorBox] public TMP_Text ballsText;
        [FoldoutGroup("Card"), ColorBox] public Image portraitCard;

        private bool _hideOnButton = true;

        private void Start()
        {
            nameText.text = name;
            powerText.text = powerValue;
            portraitCard.sprite = portrait;
        }
        public void ShowPopup()
        {
            //get a clone of the UIPopup, with the given PopupName, from the UIPopup Database
            _popup = UIPopup.GetPopup(popupName);

            //make sure that a popup clone was actually created
            if (_popup == null)
                return;

            //we assume (because we know) this UIPopup has a Title and a Message text objects referenced, thus we set their values
            _popup.Data.SetLabelsTexts(name, powerValue, healthValue, damageValue, ballsValue);

            _popup.Data.SetImagesSprites(portrait);

            //get the values from the label input fields
            // LabelButtonOne = LabelButtonOneInput.text;
            // LabelButtonTwo = LabelButtonTwoInput.text;

            //set the button labels
            // _popup.Data.SetButtonsLabels(LabelButtonOne, LabelButtonTwo);

            //set the buttons callbacks as methods
            _popup.Data.SetButtonsCallbacks(ClickButtonOne);

            //OR set the buttons callbacks as lambda expressions
            _popup.Data.SetButtonsCallbacks(() => { ClickButtonOne(); }/*, () => { ClickButtonTwo(); }*/);

            //if the developer did not enable at least one button to hide it, make the UIPopup hide when its Overlay is clicked
            if (!_hideOnButton)
            {
                _popup.HideOnClickOverlay = true;
                // DDebug.Log("Popup '" + PopupName + "' is set to close when clicking its Overlay because you did not enable any hide option");
            }

            _popup.Show(); //show the popup
        }

        private void ClickButtonOne()
        {
            // DDebug.Log("Clicked button ONE: " + LabelButtonOne);
            if (_hideOnButton) ClosePopup();
        }

        private void ClosePopup()
        {
            if (_popup != null) _popup.Hide();
        }
    }
}