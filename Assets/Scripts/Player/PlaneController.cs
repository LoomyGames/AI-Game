using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    public float forwardSpeed = 25f; // plane controller settings (spaceship controls basically)
    public float hoverSpeed = 5f;
    private float activeForwardSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f;
    private float hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    public int health = 100; //player stats
    public int kills = 0;
    public int ammo = 1000;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * 0.5f; //get the coordinates of the center of the screen
        screenCenter.y = Screen.height * 0.5f;

        Cursor.lockState = CursorLockMode.Confined;//lock the cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x; //get the mouse's input based on the location of the cursor
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y; //get the mouse distance from the center
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f); //clamp it so it doesn't accelerate the rotation indefinitely

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Horizontal"), rollAcceleration * Time.deltaTime); //get the roll input

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed
            * Time.deltaTime, -rollInput * rollSpeed * Time.deltaTime, Space.Self); //apply the rotation based on the roll input

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed,
            forwardAcceleration * Time.deltaTime); //get the forward speed based on key presses
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed,
            hoverAcceleration * Time.deltaTime); //get the lift speed based on key presses

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime; //update the player position with the forward
        transform.position += transform.up * activeHoverSpeed * Time.deltaTime;        //and upward/downward forces

        if(health <= 0) //if the health is 0, reload the whole scene 
        {
            SceneManager.LoadScene(1);
        }
    }
}
