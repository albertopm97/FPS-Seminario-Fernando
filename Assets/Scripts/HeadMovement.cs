using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeadMovement : MonoBehaviour
{
    //Referencia al jugador
    public GameObject body;
    float rotX = 0f;

    public float mouseSensibility;

    // Start is called before the first frame update
    void Start()
    {
        //Bloqueamos el cursor al jugar
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        //Rotamos el cuerpo en x y la camara en y (Izda y dcha giramos el cuerpo, arriba y abajo simulamos cuello rotando camara
        rotX = rotX - mouseY;

        //Importante limitar la rotacion de la camara para no "romperte el cuello" usando Clamp
        rotX = Mathf.Clamp(rotX, -90f, 90f);

        //Rotacion camara
        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
        //
        body.transform.Rotate(0f, mouseX, 0f);

    }
}