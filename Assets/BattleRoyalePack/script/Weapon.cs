using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    ////Obiekt systemu cz¹steczkowego, który pozostawi œlady po kulach
    [SerializeField] protected GameObject particle;
    //Kamera (pomo¿e nam znaleŸæ œrodek ekranu)
    [SerializeField] protected GameObject cam;
    //Tryb strzelania broni
    protected bool auto = false;
    //Odstêp miêdzy strza³ami i timer, który liczy czas
    protected float cooldown = 0;
    protected float timer = 0;
    //Na pocz¹tku gry ustawiamy timer na wartoœæ cooldown broni
    //To gwarantuje, ¿e pierwszy strza³ zostanie oddany bez opóŸnienia
    // liczba pocisków w magazynku
    protected int ammoCurrent;
    // maksymalna pojemnoœæ magazynka
    protected int ammoMax;
    // zapasowa amunicja
    protected int ammoBackPack;
    // zmienna u¿ywana do reprezentowania tekstu w interfejsie u¿ytkownika
    [SerializeField] TMP_Text ammoText;
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioClip bulletSound, noBulletSound, reload;

    private void Start()
    {
        timer = cooldown;
    }
    private void Update()
    {
        //Uruchamiamy timer
        timer += Time.deltaTime;
        //Jeœli gracz nacisn¹³ lewy przycisk myszy, wywo³ujemy funkcjê Strzelaj
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        AmmoTextUpdate();
        //jeœli gracz naciska klawisz R
        if (Input.GetKeyDown(KeyCode.R))
        {
            //jeœli nasz magazynek nie jest pe³ny, LUB jeœli mamy co najmniej jeden nabój w rezerwach, to
            if (ammoCurrent != ammoMax || ammoBackPack != 0)
            {
                shoot.PlayOneShot(reload);
                //aktywowanie funkcji prze³adowania z lekkim opóŸnieniem
                //mo¿esz ustawiæ opóŸnienie na dowoln¹ liczbê, któr¹ chcesz
                Invoke("Reload", 1);
            }
        }
    }
    //Sprawdzamy, czy broñ mo¿e strzelaæ
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || auto)
        {
            if (timer > cooldown)
            {
                if (ammoCurrent > 0)
                {
                    OnShoot();
                    timer = 0;
                    ammoCurrent -= 1;
                    shoot.PlayOneShot(bulletSound);
                    shoot.pitch = Random.Range(1f, 1.5f);
                }
                else { 
                        shoot.PlayOneShot(noBulletSound);
                }
            }
        }
    }
    //A ta funkcja zdefiniuje co siê dzieje, kiedy broñ strzela. Poniewa¿ ma modyfikatory protected i virtual, klasy, które od niej dziedzicz¹, bêd¹ mog³y zdefiniowaæ swoj¹ w³asn¹ logikê strzelania
    protected virtual void OnShoot()
    {
    }
    private void AmmoTextUpdate()
    {
        ammoText.text = ammoCurrent + " / " + ammoBackPack;
    }
    private void Reload()
    {
        //deklarowanie zmiennej i obliczanie liczby naboi, które powinniœmy dodaæ do magazynku
        int ammoNeed = ammoMax - ammoCurrent;
        //jeœli iloœæ zapasowych naboi, które mamy, jest wiêksza lub równa iloœci naboi potrzebnych do prze³adowania, to
        if (ammoBackPack >= ammoNeed)
        {
            //odejmowanie liczby potrzebnych naboi od rezerw
            ammoBackPack -= ammoNeed;
            //dodanie potrzebnej liczby naboi do magazynku
            ammoCurrent += ammoNeed;
        }
        //w przeciwnym razie (jeœli w rezerwach jest mniej naboi ni¿ potrzeba do pe³nego prze³adowania)
        else
        {
            //dodajemy ca³¹ nasz¹ rezerwow¹ amunicjê do magazynka
            ammoCurrent += ammoBackPack;
            //ustawiamy rezerwê amunicji na 0
            ammoBackPack = 0;
        }
    }


}

