using System.Collections.Generic;
using UnityEngine;

public class TuturialLogic : MonoBehaviour
{
    public GameObject ActivTutorial;
    //public Transform arrow;
    //public TextMeshProUGUI textBet;
    //public TextMeshProUGUI textStart;
    //public SpriteRenderer SpriteHelper;
    ////перемещать обьект к трансформеру .вращать стрелку вместе с перемещением , изменять альфу текса изменять параметр материала
    //// Start is called before the first frame update
    ///
    //static bool firstTutorial=true;
    public int tutorlaID;
    static List<bool> list = new List<bool>(7) { false,false,false,false,false,false};
    void Start()
    {
        if (list[tutorlaID] != true)
        {
            //if (firstTutorial)
            //{
            if (ActivTutorial)
            {
                ActivTutorial.SetActive(true);
                //   firstTutorial = false;
                list[tutorlaID] = true;
            }
        }
        //  SpriteRenderer mat = arrow.GetComponent<SpriteRenderer>();

        //  Sequence sec = DOTween.Sequence();
        //  //  sec.Append(mat.DOFade(0, 4)).Join(SpriteHelper.DOFade(0, 2)).Join(textBet.DOFade(0, 2)).AppendCallback(() => textStart.DOFade(1, 2));
        //  mat.DOFade(0, 4);

        //  // sec.Append(SpriteHelper.DOFade(0, 2).Join(textBet.DOFade(0, 2)).AppendCallback(() => textStart.DOFade(1, 2));
        //  SpriteHelper.DOFade(0, 2);
        //textBet.DOFade(0, 2);
        //  textStart.DOFade(1, 2);

        //mat.DOFloat(0, "Disolve_Value", 4);
        //SpriteHelper.DOFade(0, 2);
        //textBet.DOFade(0, 2);



        // TextRect.DOAnchorPos(TextTarget.position, 3);
        //Sequence sec = DOTween.Sequence();
        //sec.Append(ObjectMove.transform.DOMove(TextTarget2.position, 3)).Join(ObjectMove.transform.DORotate(Rotation, 3));

        //Sequence sec2 = DOTween.Sequence();
        //sec2.Append(text2.DOColor(Color.red, 4)).Join(text2.DOFade(0, 4));



    }
    public void Fin()
    {
        ActivTutorial.SetActive(false);

    }
    //Vector3 trSave;
    //Vector3 rot;

    void Rot()
    {
        //if (trSave == null)
        //{
        //    trSave = ObjectMove.position;
        //    rot= ObjectMove.eulerAngles;
        //}
        //else
        //{
        //    ObjectMove.position = trSave;
        //    ObjectMove.eulerAngles = rot;
        //}
        //Sequence sec = DOTween.Sequence();
        //sec.Append(ObjectMove.transform.DOMove(TextTarget2.position, 3)).Join(ObjectMove.transform.DORotate(Rotation, 3));
    }
}
