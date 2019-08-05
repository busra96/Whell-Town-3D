using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Deneme : MonoBehaviour
{
    public Rigidbody rb;
    public float yolUzunlugu;
    public float thrust = 0;
    [SerializeField] float speed2;
    [SerializeField]bool walk = false;
    [SerializeField] float massforce;
    [SerializeField] float dragforce;
    [SerializeField] float dragsecond;
    [SerializeField] GameObject obje;
    public Camera cam;
    [SerializeField] GameObject UIcontroller;

    Animator anim;
    public bool sendelemek = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        rb.mass = massforce;
        StartCoroutine("surtunme");
    }

    // Update is called once per frame
    void Update()
    {
        //Gameobject Screen Limit
        if (transform.position.z > 5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5f);
        }
        else if (transform.position.z < -5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed2, Space.Self);


        if (UIcontroller.GetComponent<BarControl>().speedef)
        {
            //tekerleğin devamlı dönüyor görüntüsü
            obje.transform.Rotate(0, 0, 30 * Time.deltaTime);

            speed2 = UIcontroller.GetComponent<BarControl>().currentval * thrust;
            sendelemek = true;

            if (!walk)
            {
                walk = true;
                StartCoroutine("yon");

            }
        }

        StartCoroutine("sendeleme");
    }

    IEnumerator yon()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                float xPos = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10)).x;
                xPos -= 0.5f;
                transform.DORotate(new Vector3(0, xPos * 20, 0), 1);
            }
            yield return null;
        }
    }

    IEnumerator surtunme()
    {
        while (true)
        {
            if (walk)
            {
                if (thrust <= 0.2f)
                {
                    thrust = 0;
                    break;
                }
                else
                {
                    thrust -= 0.2f;
                }
            }
            yield return new WaitForSeconds(dragsecond);
        }
    }

    IEnumerator sendeleme()
    {
        if (sendelemek)
        {
            if (speed2 < 25)
            {
                anim.SetTrigger("sendelemeBasladi");
            }
            else if (speed2 > 25)
            {
                anim.SetTrigger("SendelemeBitti");
            }
        }
        yield return null;
    }


    //TASLAR İLE CARPISMA OLDUGUNDA HIZ AZALICAK
    void OnTriggerEnter(Collider collision)
    {
      if(collision.gameObject.tag == "tas")
        {
            anim.SetTrigger("CarpmaBasladi");
            thrust -= 0.3f;
        }  
    }

    //Taslar ile carpisma bitince olan kısım
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "tas")
        {
            anim.SetTrigger("CarpmaBitti");
            
        }
    }


}
