using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public float thrust = 0;
    public Rigidbody rb;
    bool walk = false;
    private float speed;

    bool yonver = true;

    [SerializeField] float dragforce;
    [SerializeField] float massforce;
    [SerializeField] int dragsecond;


    public float xpos1, xpos2;
    public float donushizi;

    //[SerializeField] private Slider slider;
    [SerializeField] GameObject UIcontroller;

    public Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = massforce;
    }

    void Update()
    {
        
        //Gameobject Screen Limit
        if(transform.position.z > 5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5f);
        }
        else if(transform.position.z < -5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
        }

        
        if (UIcontroller.GetComponent<BarControl>().speedef)
        {

            speed = UIcontroller.GetComponent<BarControl>().currentval * thrust;
            transform.Rotate(0, 0, -30 * Time.deltaTime);

            if (!walk)
            {
                rb.AddForce(Vector3.right * speed);
               
                walk = true;
                StartCoroutine("surtunme");
                StartCoroutine("yon");
            }


            //tekerleğin sağ ve sol yön
            if (transform.rotation.x > 0)
            {
                Debug.Log("sol dön");
                rb.AddForce(new Vector3(1, 0, 1.5f) * donushizi);
              
            }
            else if (transform.rotation.x < 0)
            {
                Debug.Log("sağ yap");
                rb.AddForce(new Vector3(1, 0, -1.5f) * donushizi);
               

            }


            if (rb.velocity.z > 20)
            {
                Vector3 vel = rb.velocity;
                vel.z = 20;
                rb.velocity = vel;
            }

            if (rb.velocity.z < -20)
            {
                Vector3 vel = rb.velocity;
                vel.z = -20;
                rb.velocity = vel;
            }

            

        }
    }

    IEnumerator surtunme()
    {
        yield return new WaitForSecondsRealtime(dragsecond);
        rb.drag += dragforce;
    }

    IEnumerator yon()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseP = cam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
                float xPos = mouseP.x;

                xPos -= xpos1;
                xPos *= xpos2;

                transform.rotation = Quaternion.Euler(transform.rotation.x, xPos, transform.rotation.z);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

}
