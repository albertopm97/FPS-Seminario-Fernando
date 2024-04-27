using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Estadisticas")]
    public int maxHealth;
    int actualHealth;
    bool haveWeapon;
    bool shooting;

    [Header("Referencias")]
    public GameObject rifle; //prefab rifle
    Rigidbody rb;   //Rigidbody

    //Disparo
    public Transform BulletOrigin;
    public GameObject bulletPrefab;

    [Header("UI")]
    public Scrollbar sb;



    public bool touchingGround;
    //Alante o atras
    float speedZ;

    //Movimiento lateral
    float speedX;

    [Header("Movimiento")]
    //Publicas para poder editar los multiplicadores en editor;
    public float multiplyWalk;
    public float multiplyRun;
    public float jumpForce;

    //Al activarse (listo para el juego)
    private void OnEnable()
    {
        EventsManager.OnRiflePicked += () => { rifle.SetActive(true); haveWeapon = true;  };
        EventsManager.OnModifyHealth += modifyHealth;   // otro metodo -> EventsManager.OnModifyHealth += (int value) => {// funcion inline };
    }

    //Al desactivarse
    private void OnDisable()
    {
        EventsManager.OnRiflePicked -= () => { rifle.SetActive(true); haveWeapon = true; };
        EventsManager.OnModifyHealth -= modifyHealth;   // otro metodo ->   EventsManager.OnModifyHealth -= (int value) => {//funcion inline};

    }

    // Start is called before the first frame update
    void Start()
    {
        actualHealth = maxHealth;

        rb = GetComponent<Rigidbody>();

        touchingGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Para evitar movimiento sin pulsar nada
        speedX = 0f;
        speedZ = 0f;

        //Andar alante o atras
        if(Input.GetKey(KeyCode.W))
        {
            speedZ = multiplyWalk;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speedZ = -multiplyWalk;
        }

        //Andar derecha o izquierda
        if (Input.GetKey(KeyCode.D))
        {
            speedX = multiplyWalk;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            speedX = -multiplyWalk;
        }

        //Si estamos apretando shift, aplicamos también el multiplicador de correr
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedX *= multiplyRun;
            speedZ *= multiplyRun;
        }

        //Saltamos al pulsar espacio
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            if (touchingGround)
            {
                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
            }
        }

        if(haveWeapon)
        {
            //Gestion del disparo
            if (Input.GetMouseButton(0))
            {
                if(!shooting)
                {
                    StartCoroutine(shoot(0.2f));
                }
            }
        }
    }

    //El movimiento lo aplicamos en fixed porque se llama mas veces, evitando bugs y traspasos de paredes
    private void FixedUpdate()
    {
        //aplicamos los imputs para movernos
        transform.Translate(speedX * Time.deltaTime, 0f, speedZ * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            touchingGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            touchingGround = false;
        }
    }

    IEnumerator shoot(float time)
    {
        shooting = true;
        //Creamos la bala y seteamos su posicion y su rotacion (sin hacer hijo para evitar bugs de movimiento)
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = BulletOrigin.position;
        bullet.transform.rotation = BulletOrigin.rotation;
        Destroy(bullet, 10f);

        yield return new WaitForSeconds(time);

        shooting = false;
    }
    
    void modifyHealth(int value)
    {
        actualHealth += value;

        if(actualHealth > maxHealth)
        {
            actualHealth = maxHealth;
        }

        if(actualHealth < 0)
        {
            actualHealth = 0;
        }

        //Igual se puede sustituir el codigo anterior por Mathf.Clamp(acualHealth, 0, maxHealth)
        sb.size = (float)actualHealth / (float)maxHealth;
    }
}
