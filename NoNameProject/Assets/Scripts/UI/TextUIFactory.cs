using TMPro;
using UnityEngine;

public class TextUIFactory : UIFactory
{
    private Transform _container;

    public TextUIFactory(Transform container)
    {
        _container = container;
    }

    public override UIElement CreatePopup(object text)
    {
        var prefap = Resources.Load<GameObject>("Prefaps/Hello Text");
        var obj = GameObject.Instantiate(prefap);
        var textObject = obj.AddComponent<BaseUIElement>();
        textObject.transform.SetParent(_container);
        textObject.GetComponent<TextMeshProUGUI>().text = (string)text;
        textObject.transform.position = new Vector3(0f, 0f);
        return textObject;
    }
}

