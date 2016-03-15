using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //GameObjects
    public GameObject MyCanvas;
    public GameObject TextaAffiche;
    public GameObject Pellet;
    public GameObject Pellet2;


    //Text
    public Text textAffiche;
    public Text gold;


    //Bool
    private bool IsChosing;
    private bool Once;
    private bool Twice;
    private bool AfficheCanvas;
    private bool AfficheText;
    private bool InstantiateOnce;
    private bool OnlyOnce;

    //Autre
    private Vector2 movement;
    private float time;
    private string MyChoice;

    // Use this for initialization
    void Start()
    {
        OnlyOnce = true;
        MyChoice = "";
        TextaAffiche.SetActive(false);
        AfficheText = false;
        AfficheCanvas = true;
        MyCanvas.SetActive(false);
        IsChosing = false;
        Once = false;
        Twice = false;
        InstantiateOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        gold.GetComponent<Text>().text = "Gold : " +PlayerPrefs.GetFloat("Gold").ToString();
        if (AfficheText)
        {
            TextaAffiche.SetActive(true);
        }
        if (MyChoice == "Tuer")
        {
            Tuer();
        }
        if (MyChoice == "EssayerdePasser")
        {
            EssayerdePasser();
        }
        if (MyChoice == "Donnerdelor")
        {
            Donnerdelor();
        }
        if (MyChoice == "Blesser")
        {
            Blesser();
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x < 0)
        {
            movement = new Vector2(5, 0);
            GetComponent<Rigidbody2D>().velocity = movement;
        }
        else
        {
            if (AfficheCanvas)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                MyCanvas.SetActive(true);
                IsChosing = true;
            }
        }
        if (IsChosing)
        {
            Choice();
        }

    }

    //Fais ton choix
    private void Choice()
    {
        float inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            MyCanvas.SetActive(false);
            MyChoice = "Tuer";
            IsChosing = false;
            Tuer();
            AfficheCanvas = false;
        }
        if (inputX < 0)
        {
            MyCanvas.SetActive(false);
            MyChoice = "Blesser";
            IsChosing = false;
            Blesser();
            AfficheCanvas = false;
        }
        float inputY = Input.GetAxis("Vertical");
        if (inputY > 0)
        {
            MyCanvas.SetActive(false);
            MyChoice = "EssayerdePasser";
            IsChosing = false;
            EssayerdePasser();
            AfficheCanvas = false;
        }
        if (inputY < 0)
        {
            MyCanvas.SetActive(false);
            MyChoice = "Donnerdelor";
            IsChosing = false;
            Donnerdelor();
            AfficheCanvas = false;
        }
    }

    //Conséquence du choix
    private void Tuer()
    {
        movement = new Vector2(5, 0);
        GetComponent<Rigidbody2D>().velocity = movement;
        if (transform.position.x > 10)
        {
            AfficheText = true;
            MyChoice = "";
        }
        if (InstantiateOnce)
        {
            Instantiate(Pellet, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            InstantiateOnce = false;
            textAffiche.GetComponent<Text>().text = "Vous venez de tuer un joueur rouge\nLa haine de son clan à votre égard augmente";
        }
    }
    private void EssayerdePasser()
    {
        movement = new Vector2(5, 0);
        textAffiche.GetComponent<Text>().text = "Vous avez épargné joueur rouge\nSon clan saura s'en souvenir";

        GetComponent<Rigidbody2D>().velocity = movement;
        if (transform.position.x > 10)
        {
            AfficheText = true;
            MyChoice = "";
        }
    }
    private void Blesser()
    {
        movement = new Vector2(5, 0);
        GetComponent<Rigidbody2D>().velocity = movement;
        if (transform.position.x > 10)
        {
            AfficheText = true;
            MyChoice = "";
        }
        if (InstantiateOnce)
        {
            Instantiate(Pellet2, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.Euler(0, 0, 0));
            InstantiateOnce = false;
            textAffiche.GetComponent<Text>().text = "Vous venez de blesser un joueur rouge\nLa haine de son clan à votre égard augmente quelque peu";
        }
    }
    private void Donnerdelor()
    {
        textAffiche.GetComponent<Text>().text = "Vous venez de perdre 10gold";
        if (OnlyOnce)
        {
            PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") - 10);
            OnlyOnce = false;
        }
        movement = new Vector2(5, 0);
        GetComponent<Rigidbody2D>().velocity = movement;
        if (transform.position.x > 10)
        {
            AfficheText = true;
            MyChoice = "";
        }
    }
}
