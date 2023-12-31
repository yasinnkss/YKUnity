using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController thisCharacther;
    public float arrow;
    public float upArrow;
    [SerializeField]
    private float speed = 5;
    GameObject cam;
    Vector3 camOffset;
    private Animator playerAnim;

    // Start is called before the first frame update
    void Start()
    {
        thisCharacther = gameObject.GetComponent<CharacterController>();// karakter kontrol componentini kullanacağız
        cam = GameObject.Find("Main Camera");// main kamerayı kullanacağız
        camOffset = cam.GetComponent<Transform>().position; // kamera pozsiyonunu kullanarak kamerayı takip ettireceğiz
        playerAnim = gameObject.GetComponent<Animator>();// player nesnemiz için animasyon ayarlayacağız
    }

    // Update is called once per frame
    void Update()
    {
        Movement();//karakter hareket fonksiyonu
    }

    private void LateUpdate()
    {
        camFollow(); // kamera takip
                     // late update içerisinde olma nedenini tam olarak bilmiyorum
                     // ancak update ya da fixed update içerisinde kullanınca kamerada titreme sorunu oluyor
                     // belki de sebebi oyuncunun güncel pozisyonunu, pozisyonu güncellendikten sonra almasıdır
    }

    void Movement()
    {
        upArrow = Input.GetAxis("Vertical") * Time.deltaTime * speed;// S ve W tuşlarını kullanarak -1("S") ve +1("W") değerler döndürüyor.
                                                                     // (Bizim oyunumuzda vertical Z eksenini ifade ediyor)
        arrow = Input.GetAxis("Horizontal") * Time.deltaTime * speed;// A ve D tuşlarını kullanarak -1("A") ve +1("D") değerler döndürüyor.
        thisCharacther.Move(new Vector3(arrow, 0, upArrow));
        if (upArrow != 0 || arrow != 0)
        {
            playerAnim.SetFloat("speed_f", 1f); // hareket tuşları kullanılırken animasyonu başlatır
        }
        else
        {
            playerAnim.SetFloat("speed_f", 0f); // hareket tuşlarına basılmıyorsa hareketi durdurur
                                                // ayrıca animasyonda has exit time kapalı
        }
    }

    // camera takip kodu
    void camFollow()
    {
        Vector3 ofset = camOffset + thisCharacther.transform.position; // oyuncu ile kamera arasındaki mesafeyi ayarlıyoruz
        cam.transform.position = ofset; // kameranın pozisyonunu o mesafeye eşitliyoruz.
    }
}
