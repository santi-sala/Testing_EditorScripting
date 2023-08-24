using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace PipoTools.Localisation {
    public class LocaliseObject : MonoBehaviour
    {
        [Header("Localisation")]
        public string key;
        [Header("Affected Components")]
        public bool textUI;
        public bool buttonUI;
        public bool imageUI;
        public bool texture2d;
        [Header("Modifiers")]
        public bool useButtonToSetLocale;
        public int localisationIndex;
        public bool goToNextSceneOnClick;
        public string nextSceneName;

        void Start()
        {
            SetLocalisedObject();
        }

        public void SetLocalisedObject()
        {
            if (textUI == true)
            {
                TextMeshProUGUI comp = GetComponent<TextMeshProUGUI>();
                comp.text = Manager_Localisation.instance.GetLocalisedValue(key);
            }

            if (buttonUI == true && useButtonToSetLocale == false)
            {
                TextMeshProUGUI comp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                comp.text = Manager_Localisation.instance.GetLocalisedValue(key);
            }

            if (buttonUI == true && useButtonToSetLocale == true)
            {
                TextMeshProUGUI comp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                comp.text = Manager_Localisation.instance.availableLanguages[localisationIndex].name;
                Button btn = GetComponent<Button>();
                btn.onClick.AddListener(() => Manager_Localisation.instance.LoadLocalisedText(Manager_Localisation.instance.availableLanguages[localisationIndex].fileName));
                btn.onClick.AddListener(() => Manager_Localisation.instance.saveDefault(localisationIndex));
                if (goToNextSceneOnClick)
                {
                    if (PlayerPrefs.HasKey("PipoToolsLocalisationIndex"))
                    {
                        Manager_Localisation.instance.loadDefault();
                        SceneManager.LoadScene(nextSceneName);
                    }
                    else
                    {
                        btn.onClick.AddListener(() => SceneManager.LoadScene(nextSceneName));
                    }
                }
            }
            if (imageUI == true)
            {
                Image comp = GetComponent<Image>();
                comp.sprite = (Sprite)Resources.Load(Manager_Localisation.instance.GetLocalisedValue(key));
            }
            if (texture2d == true)
            {
                Texture2D comp = GetComponent<Texture2D>();
                comp = (Texture2D)Resources.Load(Manager_Localisation.instance.GetLocalisedValue(key));
            }
        }

    }
}
