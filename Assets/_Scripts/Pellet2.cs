using UnityEngine;
using System.Collections;

public class Pellet2 : MonoBehaviour {

    private float speed;
    public Sprite blesse;

    void Start()
    {
        speed = 1;
        GetComponent<Rigidbody2D>().velocity = new Vector2(6, 0) * speed;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ennemi")
        {
            col.gameObject.GetComponent<SpriteRenderer>().sprite = blesse;
            Destroy(gameObject);
        }
    }
}
