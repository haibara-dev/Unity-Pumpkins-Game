using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public Text Abobora;
    int contador = 0;
    public CharacterController controller;

    public float speed = 8f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Animation anima;
    public GameObject meshCharacter;

    private int pumpkins_value;

    void Start()
    {
        anima = meshCharacter.GetComponent<Animation>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime); // rota��o bonitinha
            transform.rotation = Quaternion.Euler(0f, angle, 0f); // setar rota��o

            controller.Move(direction * speed * Time.deltaTime);
        }

        // ANIMA��O
        if (direction.x == 0.0f && direction.z == 0.0f)
        {
            anima.CrossFade("idle");
        }
        else
        {
            anima.CrossFade("run");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pumpkin")
        {
            GetComponent<AudioSource>().Play();
            contador++;
            pumpkins_value++;
            Abobora.text = contador.ToString();

            if (pumpkins_value == 3)
            {
                SceneManager.LoadScene("ObjetivoCompleto");
            }

        }
    }

    
}
