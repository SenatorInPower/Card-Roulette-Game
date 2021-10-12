using UnityEngine;
namespace Bingo
{
    public class ClickButton : MonoBehaviour
    {
        void OnMouseDown()
        {
            GameMNG.Instance.StartGame();
        }
    }
}