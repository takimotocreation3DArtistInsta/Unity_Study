using UnityEngine;

public class Example : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Awake was called!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start was called!");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update is called!");
    }
    
    // 50 times per second
    private void FixedUpdate()
    {
        
    }
}
