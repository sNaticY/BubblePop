using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Canvas _uiCanvas;
    [SerializeField] private SettingsMenuController _settingsPanel;
    
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _settingsPanel.Initialize();
        
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    private void OnSettingsButtonClicked()
    {
        _settingsPanel.gameObject.SetActive(true);
    }

    private void OnPlayButtonClicked()
    {
        _uiCanvas.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }
}
