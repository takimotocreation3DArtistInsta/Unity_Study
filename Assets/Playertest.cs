using System.Runtime.CompilerServices;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public string playerName = "Bob the hero"; // This box (variable) called "playername" holds the text "Bob the hero".
    public int age = 25; // This box (variable) called "age" holds the number "25".
    public int characterLevel = 80;
    public float moveSpeed = 2.5f; // This box (variable) called "moveSpeed" holds the number "2.5f".
    public bool gameOver = true; // This box (variable) called "gameOver" holds the true/false value.

    public Rigidbody2D rb;

    public int currentHp = 100;

    private void Start()
    {
        TakeDamage(25);
    }

    private void TakeDamage(int damage)
    {
        currentHp = currentHp - damage;
    }


}
