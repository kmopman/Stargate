using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {


    //GameObject
    [SerializeField]
    private GameObject _backButton;
    //GameObject

    [SerializeField]
    private GameObject _creditsNames;

    [SerializeField]
    private GameObject _howToPlayBox;

    [SerializeField]
    private GameObject _playButton;
    [SerializeField]
    private GameObject _htpButton;
    [SerializeField]
    private GameObject _creditsButton;
    //GameObject

    void Start()
    {
        _backButton.SetActive(false);
        _creditsNames.SetActive(false);
        _howToPlayBox.SetActive(false);
    }

    void Update()
    {
        BackButtonHTP();
    }

    public void PlayButton()
    {
        Application.LoadLevel("Master"); //Load Level
    }

    public void HowToPlayButton()
    {
        _playButton.SetActive(false);
        _htpButton.SetActive(false);
        _creditsButton.SetActive(false);
        _backButton.SetActive(true);
        _howToPlayBox.SetActive(true);
    }

    public void CreditsButton()
    {
        _playButton.SetActive(false);
        _htpButton.SetActive(false);
        _creditsButton.SetActive(false);
        _creditsNames.SetActive(true);
        _backButton.SetActive(true);

    }

    public void BackButtonHTP()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _creditsNames.SetActive(false);
            _playButton.SetActive(true);
            _htpButton.SetActive(true);
            _creditsButton.SetActive(true);
            _howToPlayBox.SetActive(false);
            _backButton.SetActive(false);
        }
        
    }
}
