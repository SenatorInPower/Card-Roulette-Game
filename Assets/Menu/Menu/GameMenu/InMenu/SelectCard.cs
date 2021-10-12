using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Assets.Menu.Scr
{
    public class SelectCard : CardMenu, IPointerEnterHandler, IPointerExitHandler
    {
        SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            startLerps = transform.localScale;

        }
        public void OnPointerEnter(PointerEventData eventData)
        {

            AudioEvent.AudioEvents.AudioPlay(AudioAction.ClickToCard);

            spriteRenderer.material = materialSelect;
            selectCard = this;
            stopCuratine = false;
            StartCoroutine(ScalerCard());
        }




        public void OnPointerExit(PointerEventData eventData)
        {
            spriteRenderer.material = materialStart;
            stopCuratine = true;

        }
        bool stopCuratine = false;


        Vector3 startLerps;

        IEnumerator ScalerCard()
        {
            int rever = 1;
            float timer = 0;
            Vector3 toLerp = startLerps * 1.1f;
            Vector3 startLerp = startLerps;
            while (true)
            {

                transform.localScale = Vector3.Lerp(startLerp, toLerp, timer);
                if (stopCuratine)
                {

                    if (selectCard != this)
                    {

                        //stopCuratine = false;
                        rever = -1;

                    }

                }

                timer += Time.deltaTime * rever * 3;

                //if (timer > 1)
                //{

                //}
                if (timer < 0)
                {

                    break;
                }

                yield return null;
            }
        }


        private void OnMouseUp()
        {
            if (stopCuratine == false)
            {
                 AudioEvent.AudioEvents.AudioPlay(AudioAction.ClickToCard);

                NameScen scenName = GetComponent<NameScen>();
                SceneManager.LoadScene(scenName.scenName);
                DisableInLevel.ScenGames = scenName.ScenGame;
            }
        }

    }
}