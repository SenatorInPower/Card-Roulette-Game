using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace BlackDjack
{
    public class Menu : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;

        private void Start()
        {
            _playButton.onClick.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}