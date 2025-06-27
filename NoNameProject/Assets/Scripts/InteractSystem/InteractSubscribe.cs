using Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractSubscribe : IInitzializable, IDisposable
{
    private BuildingsSubscribe _buildingSubscribe;
    private UIFactory _uiFactory;
    private List<Popup> _popups;

    public InteractSubscribe(UIFactory uIFactory, BuildingsSubscribe buildingsSubscribe)
    {
        _uiFactory = uIFactory;
        _popups = new List<Popup>();

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

        Debug.Log("Interact Initzialize");
    }

    private void OnIntearct(object obj)
    {
        var popup = _uiFactory.CreatePopup(obj);
        _popups.Add(popup);
    }

    public void Dispose()
    {
        _buildingSubscribe.InteractSignalAdded -= InteractInit;
    }

    private void InteractDispose()
    {
        foreach (var item in _buildingSubscribe.InteractSignals)
            item.Interact -= OnIntearct;
    }
}

