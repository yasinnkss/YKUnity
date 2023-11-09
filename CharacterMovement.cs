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
        thisCharacther = gameObject.GetComponent<CharacterController>();// karakter kontrol componentini kullanaca��z
        cam = GameObject.Find("Main Camera");// main kameray� kullanaca��z
        camOffset = cam.GetComponent<Transform>().position; // kamera pozsiyonunu kullanarak kameray� takip ettirece�iz
        playerAnim = gameObject.GetComponent<Animator>();// player nesnemiz i�in animasyon ayarlayaca��z
    }

    // Update is called once per frame
    void Update()
    {
        Movement();//karakter hareket fonksiyonu
    }

    private void LateUpdate()
    {
        camFollow(); // kamera takip
                     // late update i�erisinde olma nedenini tam olarak bilmiyorum
                     // ancak update ya da fixed update i�erisinde kullan�nca kamerada titreme sorunu oluyor
                     // belki de sebebi oyuncunun g�ncel pozisyonunu, pozisyonu g�ncellendikten sonra almas�d�r
    }

    void Movement()
    {
        upArrow = Input.GetAxis("Vertical") * Time.deltaTime * speed;// S ve W tu�lar�n� kullanarak -1("S") ve +1("W") de�erler d�nd�r�yor.
                                                                     // (Bizim oyunumuzda vertical Z eksenini ifade ediyor)
        arrow = Input.GetAxis("Horizontal") * Time.deltaTime * speed;// A ve D tu�lar�n� kullanarak -1("A") ve +1("D") de�erler d�nd�r�yor.
        thisCharacther.Move(new Vector3(arrow, 0, upArrow));
        if (upArrow != 0 || arrow != 0)
        {
            playerAnim.SetFloat("speed_f", 1f); // hareket tu�lar� kullan�l�rken animasyonu ba�lat�r
        }
        else
        {
            playerAnim.SetFloat("speed_f", 0f); // hareket tu�lar�na bas�lm�yorsa hareketi durdurur
                                                // ayr�ca animasyonda has exit time kapal�
        }
    }

    // camera takip kodu
    void camFollow()
    {
        Vector3 ofset = camOffset + thisCharacther.transform.position; // oyuncu ile kamera aras�ndaki mesafeyi ayarl�yoruz
        cam.transform.position = ofset; // kameran�n pozisyonunu o mesafeye e�itliyoruz.
    }
}
