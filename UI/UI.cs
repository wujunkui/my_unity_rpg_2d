using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private GameObject mainUI;
        [SerializeField] private GameObject contentParent;
        [SerializeField] private GameObject inGameUI;
        public ToolTip.ItemToolTip itemToolTip;
        public ToolTip.SkillToolTip skillToolTip;
        private bool mainUIActive;

        private void Start()
        {
            OpenInGameUI();
        }

        private void Update()
        {
            if(!Input.GetKeyDown(KeyCode.Escape))
                return;
            SwitchUi();
        }

        private void SwitchUi()
        {
            if(!mainUIActive)
            {
                OpenMainUI();
            }
            else
            {
                OpenInGameUI();
            }
        }

        private void OpenInGameUI()
        {
            Time.timeScale = 1;
            mainUI.SetActive(false);
            mainUIActive = false;
            inGameUI.SetActive(true);
        }

        private void OpenMainUI()
        {
            Time.timeScale = 0;
            inGameUI.SetActive(false);
            mainUI.SetActive(true);
            for (int i = 0; i < mainUI.transform.childCount; i++)
            {
                mainUI.transform.GetChild(i).gameObject.SetActive(true);
            }
            SwitchTo(contentParent.transform.GetChild(0).gameObject);
            mainUIActive = true;
        }

        public void SwitchTo(GameObject _menu)
        {
            for (int i = 0; i < contentParent.transform.childCount; i++)
            {
                contentParent.transform.GetChild(i).gameObject.SetActive(false);
            }

            
            _menu?.SetActive(true);
            

        }
    }
}