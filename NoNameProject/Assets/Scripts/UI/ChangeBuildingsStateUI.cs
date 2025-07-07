using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeBuildingsStateUI : UIElement, IDisposable
{
    public Action<string> BuildingFactoryChanged;

    private Button _button;
    private string _buildingState;

    public void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    public void Initzialize(string buildingsState)
    {
        _buildingState = buildingsState;

        _button.onClick.AddListener(() =>
        {
            BuildingFactoryChanged?.Invoke(_buildingState);
        });

        BuildingFactoryChanged?.Invoke(_buildingState);
    }

    public void Dispose()
    {
        _button.onClick.RemoveListener(() =>
        {
            BuildingFactoryChanged?.Invoke(_buildingState);
        });
    }
}
