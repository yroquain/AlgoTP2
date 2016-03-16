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
    public GameObject ArrowUp;
    public GameObject ArrowDown;
    public GameObject ArrowLeft;
    public GameObject ArrowRight;

    //Sprites
    public Sprite EnnB;
    public Sprite EnnR;
    public Sprite EnnV;

    //Text
    public Text textAffiche;
    public Text gold;
    public Text textChoix;
    public Text TextUp;
    public Text TextLeft;
    public Text TextRight;
    public Text TextDown;


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
    private string PersEnn;
    public string ClanEnn;
    private float time;
    private string MyChoice;
    private float TypePersonnage;
    private float TypeClan;
    private string[] Clan;

    // Use this for initialization
    void Start()
    {
        TypeClan = Random.Range(0F, 2.9F);
        if (TypeClan < 1)
        {
            ClanEnn = "Bleu";
            GameObject.FindWithTag("Ennemi").GetComponent<SpriteRenderer>().sprite = EnnB;
        }
        else if (TypeClan < 2)
        {
            ClanEnn = "Vert";
            GameObject.FindWithTag("Ennemi").GetComponent<SpriteRenderer>().sprite = EnnV;
        }
        else if (TypeClan < 3)
        {
            ClanEnn = "Rouge";
            GameObject.FindWithTag("Ennemi").GetComponent<SpriteRenderer>().sprite = EnnR;
        }
        TypePersonnage = Random.Range(0F, 3.9F);
        if (TypePersonnage < 1)
        {
            PersEnn = "Soldat";
        }
        else if (TypePersonnage<2)
        {
            PersEnn = "Paysan";
            ArrowDown.SetActive(false);
            TextUp.GetComponent<Text>().text = "Passer sans rien faire";
        }
        else if (TypePersonnage<3)
        {
            PersEnn = "Blessé";
            ArrowLeft.SetActive(false);
            TextUp.GetComponent<Text>().text = "Laisser agoniser";
            TextDown.GetComponent<Text>().text = "Sauver";
        }
        else if (TypePersonnage<4)
        {
            PersEnn = "Voleur";
        }
        if(PlayerPrefs.GetFloat("Gold")<10 && PersEnn!="Blessé")
        {
            ArrowDown.SetActive(false);
        }
        textChoix.GetComponent<Text>().text = "Vous vous trouvez devant un " + PersEnn + " " + ClanEnn + "\nQuel est votre choix?";
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
        Clan = new string[3];
        Clan[0] = "Bleu";
        Clan[1] = "Vert";
        Clan[2] = "Rouge";
        Debug.Log(PlayerPrefs.GetFloat("RespectRouge"));
        Debug.Log(PlayerPrefs.GetFloat("RespectBleu"));
        Debug.Log(PlayerPrefs.GetFloat("RespectVert"));
        Debug.Log(PlayerPrefs.GetFloat("Gold"));
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
        if (PersEnn != "Blessé")
        {
            if (inputX < 0)
            {
                MyCanvas.SetActive(false);
                MyChoice = "Blesser";
                IsChosing = false;
                Blesser();
                AfficheCanvas = false;
            }
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
        if (PersEnn != "Paysan" && (PlayerPrefs.GetFloat("Gold")>=10 || PersEnn=="Blessé"))
        {
            if (inputY < 0)
            {
                MyCanvas.SetActive(false);
                MyChoice = "Donnerdelor";
                IsChosing = false;
                Donnerdelor();
                AfficheCanvas = false;
            }
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
            textAffiche.GetComponent<Text>().text = "Vous venez de tuer un joueur " + ClanEnn+"\nLa haine de son clan à votre égard augmente";
            if (PersEnn == "Soldat")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 5);
                Reputation(-8);
            }
            if (PersEnn == "Paysan")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 2);
                Reputation(-8);
            }
            if (PersEnn == "Blessé")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 3);
                Reputation(-5);
            }
            if (PersEnn == "Voleur")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 7);
                Reputation(-8); 
            }
        }
    }
    private void EssayerdePasser()
    {

        if (PersEnn != "Soldat" || (PersEnn == "Soldat" && PlayerPrefs.GetFloat("Respect" + ClanEnn) > 50))
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
                InstantiateOnce = false;
                textAffiche.GetComponent<Text>().text = "Vous avez épargné joueur " + ClanEnn + "\nSon clan saura s'en souvenir";

                if (PersEnn == "Paysan")
                {
                    Reputation(+3);
                }
                if (PersEnn == "Blessé")
                {
                    Reputation(-8);
                }
                if (PersEnn == "Voleur")
                {
                    if (PlayerPrefs.GetFloat("Respect" + ClanEnn) < 40)
                    {
                        Reputation(-20);
                    }
                }
            }
        }
        else
        {
            //GameOver
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
            textAffiche.GetComponent<Text>().text = "Vous venez de blesser un joueur " + ClanEnn + "\nLa haine de son clan à votre égard augmente quelque peu";
            if (PersEnn == "Soldat")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 5);
                Reputation(-5);
            }
            if (PersEnn == "Paysan")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 2);
                Reputation(-5);
            }
            if (PersEnn == "Voleur")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") + 7);
                Reputation(-5);
            }
        }
    }
    private void Donnerdelor()
    {
        if (OnlyOnce)
        {
            if (PersEnn == "Soldat")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") - 10);
                textAffiche.GetComponent<Text>().text = "Vous venez de perdre 10gold";
            }
            if (PersEnn == "Blessé")
            {
                Reputation(15);
            }
            if (PersEnn == "Voleur")
            {
                PlayerPrefs.SetFloat("Gold", PlayerPrefs.GetFloat("Gold") - 10);
            }
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
    private void Reputation(float respect)
    {
        for(int i=0; i<3; i++)
        {
            if (Clan[i]==ClanEnn)
            {
                PlayerPrefs.SetFloat("Respect" + Clan[i], PlayerPrefs.GetFloat("Respect"+ Clan[i]) + respect);
            }
            else
            {
                PlayerPrefs.SetFloat("Respect" + Clan[i], PlayerPrefs.GetFloat("Respect"+ Clan[i]) + (respect/3));
            }
        }
    }
}
