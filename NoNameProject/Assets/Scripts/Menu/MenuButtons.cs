using Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MenuButtons : IDisposable, IInitzializable
    {
        private Button _play;
        private Button _onlineGame;
        private Button _settings;
        private Button _quit;
        private Button _help;

        public MenuButtons(Button play, 
            Button onlineGame, Button settings, 
            Button quit, Button help)
        {
            _play = play;
            _onlineGame = onlineGame;
            _settings = settings;
            _quit = quit;
            _help = help;
        }

        public void Dispose()
        {
            _play.onClick.RemoveListener(() => SceneController.OpenGame());
            _onlineGame.onClick.RemoveListener(() => Debug.Log("This is Online Game"));
            _settings.onClick.RemoveListener(() => Debug.Log("This is Settings"));
            _quit.onClick.RemoveListener(() => Debug.Log("This is Quit"));
            _help.onClick.RemoveListener(() => Debug.Log("This is Help"));
        }

        public void Initzialize()
        {
            _play.onClick.AddListener(() => SceneController.OpenGame());
            _onlineGame.onClick.AddListener(() => Debug.Log("This is Online Game"));
            _settings.onClick.AddListener(() => Debug.Log("This is Settings"));
            _quit.onClick.AddListener(() => Debug.Log("This is Quit"));
            _help.onClick.AddListener(() => Debug.Log("This is Help"));
        }
    }
}
