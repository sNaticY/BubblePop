using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Canvas _uiCanvas = null;
    [SerializeField] private SettingsMenuController _settingsController = null;
    [SerializeField] private GameController _gameController = null;
    
    [SerializeField] private Button _playButton = null;
    [SerializeField] private Button _settingsButton = null;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        _settingsController.Initialize();
        
        _playButton.onClick.AddListener(OnPlayButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    private void OnSettingsButtonClicked()
    {
        _settingsController.gameObject.SetActive(true);
    }

    private void OnPlayButtonClicked()
    {
        _uiCanvas.gameObject.SetActive(false);
        _gameController.Initialize();
        
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.RemoveAllListeners();
    }
}
