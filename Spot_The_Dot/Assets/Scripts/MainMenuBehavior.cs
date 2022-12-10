using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuBehavior : MonoBehaviour
{
    private Button _levelSelectButton;
    private Button _skinSelectButton;
    private Button _creditsButton;
    public UIDocument levelSelection;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _levelSelectButton = uiDocument.rootVisualElement.Q("levelSelect") as Button;
        _skinSelectButton = uiDocument.rootVisualElement.Q("skins") as Button;
        _creditsButton = uiDocument.rootVisualElement.Q("credits") as Button;
        


        if (_levelSelectButton != null) _levelSelectButton.RegisterCallback<ClickEvent>(ChangeToLevelSelect);
        if (_skinSelectButton != null) _skinSelectButton.RegisterCallback<ClickEvent>(ChangeToSkinSelect);
        if (_creditsButton != null) _creditsButton.RegisterCallback<ClickEvent>(ChangeToCredits);
    }

    private void OnDisable()
    {
        _levelSelectButton.UnregisterCallback<ClickEvent>(ChangeToLevelSelect);
        _skinSelectButton.UnregisterCallback<ClickEvent>(ChangeToSkinSelect);
        _creditsButton.UnregisterCallback<ClickEvent>(ChangeToCredits);
    }

    private void ChangeToLevelSelect(ClickEvent evt)
    {
        Debug.Log($"{"levelSelect"} was clicked!");
        levelSelection.enabled = true;
    }

    private void ChangeToSkinSelect(ClickEvent evt)
    {
        Debug.Log($"{"skins"} was clicked!");
    }

    private void ChangeToCredits(ClickEvent evt)
    {
        Debug.Log($"{"credits"} was clicked!");
    }
}