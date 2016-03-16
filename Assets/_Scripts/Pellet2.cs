using UnityEngine;
using System.Collections;

public class Pellet2 : MonoBehaviour {

    private float speed;
    public Sprite blesseB;
    public Sprite blesseR;
    public Sprite blesseV;

    void Start()
    {
        speed = 1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0) * speed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ennemi")
        {
            if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().ClanEnn == "Rouge")
            {
                col.gameObject.GetComponent<SpriteRenderer>().sprite = blesseR;
            }
            if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().ClanEnn == "Bleu")
            {
                col.gameObject.GetComponent<SpriteRenderer>().sprite = blesseB;
            }
            if (GameObject.FindWithTag("Player").GetComponent<PlayerController>().ClanEnn == "Vert")
            {
                col.gameObject.GetComponent<SpriteRenderer>().sprite = blesseV;
            }
            Destroy(gameObject);
        }
    }
}
