using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectBehavior : MonoBehaviour
{
    private List<Button> _levelButtons;
    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();
        short levelNumber = 1;
        Button level = null;
        while ((level = uiDocument.rootVisualElement.Q("level" + levelNumber++) as Button) != null)
        {
            _levelButtons.Add(level);
        }

        _levelButtons[0].RegisterCallback<ClickEvent>(StartLevel1);
    }

    private void OnDisable()
    {
        _levelButtons[0].UnregisterCallback<ClickEvent>(StartLevel1);
    }

    private void StartLevel1(ClickEvent evt)
    {
        Debug.Log("level 1 was clicked!");
    }
}