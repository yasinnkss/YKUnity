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
        thisCharacther = gameObject.GetComponent<CharacterController>();// karakter kontrol componentini kullanacaðýz
        cam = GameObject.Find("Main Camera");// main kamerayý kullanacaðýz
        camOffset = cam.GetComponent<Transform>().position; // kamera pozsiyonunu kullanarak kamerayý takip ettireceðiz
        playerAnim = gameObject.GetComponent<Animator>();// player nesnemiz için animasyon ayarlayacaðýz
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
                     // ancak update ya da fixed update içerisinde kullanýnca kamerada titreme sorunu oluyor
                     // belki de sebebi oyuncunun güncel pozisyonunu, pozisyonu güncellendikten sonra almasýdýr
    }

    void Movement()
    {
        upArrow = Input.GetAxis("Vertical") * Time.deltaTime * speed;// S ve W tuþlarýný kullanarak -1("S") ve +1("W") deðerler döndürüyor.
                                                                     // (Bizim oyunumuzda vertical Z eksenini ifade ediyor)
        arrow = Input.GetAxis("Horizontal") * Time.deltaTime * speed;// A ve D tuþlarýný kullanarak -1("A") ve +1("D") deðerler döndürüyor.
        thisCharacther.Move(new Vector3(arrow, 0, upArrow));
        if (upArrow != 0 || arrow != 0)
        {
            playerAnim.SetFloat("speed_f", 1f); // hareket tuþlarý kullanýlýrken animasyonu baþlatýr
        }
        else
        {
            playerAnim.SetFloat("speed_f", 0f); // hareket tuþlarýna basýlmýyorsa hareketi durdurur
                                                // ayrýca animasyonda has exit time kapalý
        }
    }

    // camera takip kodu
    void camFollow()
    {
        Vector3 ofset = camOffset + thisCharacther.transform.position; // oyuncu ile kamera arasýndaki mesafeyi ayarlýyoruz
        cam.transform.position = ofset; // kameranýn pozisyonunu o mesafeye eþitliyoruz.
    }
}
