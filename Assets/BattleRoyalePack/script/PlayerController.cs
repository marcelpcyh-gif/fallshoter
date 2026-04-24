using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    bool isGrounded = true;

    [SerializeField] Animator animator;
    [SerializeField] GameObject pistol, rifle, miniGun;
    bool isPistol, isRifle, isMiniGun;
    [SerializeField] Image pistolUI, rifleUI, miniGunUI, cusror;
    private int health;
    // Referencja do AudioSource
    [SerializeField] AudioSource characterSounds;
    // Referencja do klipu audio skoku
    [SerializeField] AudioClip jump;

    public enum Weapons
    {
        None,
        Pistol,
        Rifle,
        MiniGun
    }
    /*Weapons weapons = Weapons.None;*/


    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        currentSpeed = movementSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        if (direction.x != 0f || direction.z != 0f)
        {
            animator.SetBool("Run", true);
            // Je¿eli AudioSource nie odtwarza ¿adnych dŸwiêków i jesteœmy na ziemi, to...
            if (!characterSounds.isPlaying && isGrounded)
            {
                // Odtwarzanie dŸwiêku
                characterSounds.Play();
            }
        }
        if (direction.x == 0f && direction.z == 0f)
        {
            animator.SetBool("Run", false);
            // Wy³¹czanie dŸwiêku, gdy postaæ siê zatrzymuje
            characterSounds.Stop();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = shiftSpeed;
        }
        else
        {
            currentSpeed = movementSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            isGrounded = false;
            // Wy³¹czanie dŸwiêku biegu
            characterSounds.Stop();
            // Tworzenie tymczasowego Ÿród³a audio dla skoku
            AudioSource.PlayClipAtPoint(jump, transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && isPistol)
        {
            ChooseWeapon(Weapons.Pistol);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isRifle)
        {
            ChooseWeapon(Weapons.Rifle);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && isMiniGun)
        {
            ChooseWeapon(Weapons.MiniGun);
        }
        //Tutaj dopisz logikê dla miniguna i dla braku broni

    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        animator.SetBool("Jump", false);
    }
    public void ChooseWeapon(Weapons weapons)
    {
        animator.SetBool("Pistol", weapons == Weapons.Pistol);
        animator.SetBool("Assault", weapons == Weapons.Rifle);
        animator.SetBool("MiniGun", weapons == Weapons.MiniGun);
        animator.SetBool("NoWeapon", weapons == Weapons.None);
        pistol.SetActive(weapons == Weapons.Pistol);
        rifle.SetActive(weapons == Weapons.Rifle);
        miniGun.SetActive(weapons == Weapons.MiniGun);
        if (weapons != Weapons.None)
        {
            cusror.enabled = true;
        }
        else
        {
            cusror.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "pistol":
                if (!isPistol)
                {
                    isPistol = true;
                    ChooseWeapon(Weapons.Pistol);
                    pistolUI.color = Color.white;
                }
                Destroy(other.gameObject);
                break;
            case "rifle":
                if (!isRifle)
                {
                    isRifle = true;
                    ChooseWeapon(Weapons.Rifle);
                    rifleUI.color = Color.white;
                }
                Destroy(other.gameObject);
                break;
            case "minigun":
                if (!isMiniGun)
                {
                    isMiniGun = true;
                    ChooseWeapon(Weapons.MiniGun);
                    miniGunUI.color = Color.white;
                }
                Destroy(other.gameObject);
                break;
            default:
                break;
        }
    }
    public void ChangeHealth(int count)
    {
        // odejmowanie zdrowia
        health -= count;
        // jeœli zdrowie spadnie do zera lub ni¿ej, to...
        if (health <= 0)
        {
            // Aktywowanie animacji œmierci
            animator.SetBool("Die", true);
            // Usuniêcie broni
            ChooseWeapon(Weapons.None);
            // Wy³¹czenie skryptu PlayerController, co uniemo¿liwia ruch gracza
            this.enabled = false;
            //Przetestujemy to wkrótce, jak tylko zaimplementujemy przeciwników!
        }

    }
}

