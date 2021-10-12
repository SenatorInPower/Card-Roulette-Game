using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
public interface SharStart
{
    bool DownButtonStart {  get; set; }
    bool RaitSelect {  get; set; }
}
public class SharLogic : MonoBehaviour, SharStart
{
    public float angle = 0; // угол 
    public float radius = 0.5f; // радиус
    
    public float speed;
    public float speedCoef = 10;
    public float radiusCoef = 10;

    public Transform center;
    public Action<int> FinAction;

    private bool DownButtonStart_;
    private bool RaitSelect_;
    public bool DownButtonStart { get => DownButtonStart_; set  { if (value == true && RaitSelect) { isCircle = true; } else { DownButtonStart_ = value; } } }
    public bool RaitSelect { get => RaitSelect_; set { if (value == true && DownButtonStart) { isCircle = true; } else { RaitSelect_ = value; } } }

    private bool isCircle = false; // условие движения по кругу

    CircleCollider2D CircleCollider2D;
    int rendomSpeed;

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D = gameObject.GetComponent<CircleCollider2D>();
        rendomSpeed = Random.Range(20, 30);

    }

    bool stoper = true;
    bool stoper2 = true;
    private void Update()
    {


        if (isCircle)
        {
            angle += Time.deltaTime; // меняется плавно значение угла
            radius -= Time.deltaTime / (rendomSpeed * radiusCoef);
            speed -= Time.deltaTime / (rendomSpeed * speedCoef);
            speed = Mathf.Clamp(speed, 0.1f, 40);
            var x = Mathf.Cos(angle * speed) * radius;
            var y = Mathf.Sin(angle * speed) * radius;
            transform.position = new Vector3(x, y, 0) + center.position;


            if (radius < 1.9f)
            {
                if (stoper)
                {

                    speedCoef /= 1.7f;
                    stoper = false;

                }
                if (radius < 1.6f)
                {
                    if (stoper2)
                    {

                        speedCoef /= 1.7f;
                        stoper2 = false;
                        CircleCollider2D.enabled = true;
                    }
                }
                if (radius < 1.4)
                {

                    print(contact.GetComponent<IDSlots>().id);


                    isCircle = false;

                    FinAction.Invoke(FinMetod());

                    StartCoroutine(moveToCenter());
                }
            }
        }
    }

    IEnumerator moveToCenter()
    {
        float time = 0;
        Vector3 nowPos = transform.position;
        while (true)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {

                yield break;
            }
            transform.position = Vector3.Lerp(nowPos, contact.transform.position, time);
            yield return null;
        }
    }



    int FinMetod()
    {
        return contact.GetComponent<IDSlots>().id;
    }
    internal GameObject contact;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isCircle == true)
        contact = collision.gameObject;


    }
}