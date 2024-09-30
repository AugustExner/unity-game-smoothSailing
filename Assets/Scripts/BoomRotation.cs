using Unity.VisualScripting;
using UnityEngine;

public class BoomRotation : MonoBehaviour
{
    public Transform BoomPivot; // Reference to the mast (or boom pivot)
    public float rotationSpeed = 50f; // Speed of rotation

    // Angle limits
    public float minAngle = 0f;
    public float maxAngle = 90f;

    private float currentAngle = 0f; // Current rotation angle

    float verticalInput;

    public float windDirection = 90.0f;
    public float windSpeed = 1.0f;

    float normalizedRelativeBoatDirection;

    float relativeBoatDirection; // Boat's direction relative to the wind

    int[] polarDiagram = { 0, 327, 638, 916, 1145, 1312, 1429, 1528, 1664, 1747, 1801, 1829, 1856, 1832, 1723, 1406, 1205, 1075, 947 };
    float polarDiagramMaxValue = 1856;

    public float minSpeed = 0.0f;
    public float maxSpeed = 1.0f;

    float movementFactor;

    void Update()
    {
        float boatRotation = transform.eulerAngles.y;

        // Calculate boat's direction relative to the wind
        relativeBoatDirection = NormalizeAngle(boatRotation - windDirection);

        normalizedRelativeBoatDirection =Mathf.Abs(relativeBoatDirection);
        //float normalizedWindDirection = NormalizeAngle(windDirection);

        //float angleDifference = NormalizeAngle(normalizedBoatDirection - normalizedWindDirection);

        verticalInput = Input.GetAxis("Vertical");

        currentAngle += verticalInput * 60f * Time.deltaTime;

        // Clamp the angle between minAngle and maxAngle
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        if (relativeBoatDirection > 0)
        {
            // Starboard Side
            // Rotate around the mast at the clamped angle
            BoomPivot.transform.localRotation = Quaternion.Euler(0, -currentAngle, 0);
        }
        else
        {
            // Port Side
            // Rotate around the mast at the clamped angle
            BoomPivot.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }

        Speed();
    }

    // Function to normalize angles to the range of 0-360
    private float NormalizeAngle(float angle)
    {
        angle = Mathf.Repeat(angle, 360f); // Ensure the angle is between 0 and 360
        if (angle > 180f)
        {
            angle -= 360f; // Convert angles greater than 180 to negative counterparts
        }
        return angle;
    }

    private void Speed()
    {
        float trimFactor = 0;
        float polarSpeedFactor = 0;
        Debug.Log("Boat Direction: " + relativeBoatDirection);

        float heading = Mathf.Abs(normalizedRelativeBoatDirection );

        if (normalizedRelativeBoatDirection > 20)
        {
            trimFactor = Mathf.Abs((180 - Mathf.Abs(2 * currentAngle - heading)) / 180);
        }
        polarSpeedFactor = polarDiagram[((int)heading / 10)] / polarDiagramMaxValue;
        float boatSpeed = (maxSpeed - minSpeed) * polarSpeedFactor * trimFactor + minSpeed;

        Debug.Log("Heading: " + heading + " | PolarFactor: " + polarSpeedFactor + " | Speed:" + boatSpeed + " | BoomAngle: " + currentAngle + " | TrimFactor: " + trimFactor);

        movementFactor = Mathf.Lerp(movementFactor, boatSpeed, Time.deltaTime / 8);
        transform.Translate((-boatSpeed * movementFactor) * Time.deltaTime, 0.0f, 0.0f);
    }
}
