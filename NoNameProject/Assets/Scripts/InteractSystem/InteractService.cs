using Interfaces;
using System;
using UnityEngine;

public class InteractService : IInitzializable, IDisposable
{
    private BuildingsService _buildingSubscribe;
    private UIFactory _uiFactory;
    private UIElement _interactPopup;

    public InteractService(UIFactory uIFactory, BuildingsService buildingsSubscribe)
    {
        _uiFactory = uIFactory;
        _buildingSubscribe = buildingsSubscribe;
    }

    public void Initzialize()
    {
        _buildingSubscribe.InteractSignalAdded += InteractInit;
    }

    private void InteractInit()
    {
        foreach (var item in _buildingSubscribe.InteractSignals)
            item.Interact += OnIntearct;
    }

    private void OnIntearct(object obj)
    {
        if(_interactPopup != null)
            GameObject.Destroy(_interactPopup.gameObject);

        _interactPopup = null;
        var popup = _uiFactory.CreatePopup(obj);
        _interactPopup = popup;
    }

    public void Dispose()
    {
        _buildingSubscribe.InteractSignalAdded -= InteractInit;
        InteractDispose();
    }

    private void InteractDispose()
    {
        foreach (var item in _buildingSubscribe.InteractSignals)
            item.Interact -= OnIntearct;
    }
}

