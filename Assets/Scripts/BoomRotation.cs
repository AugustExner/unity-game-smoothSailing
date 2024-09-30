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

    float normalizedBoatDirection;

    int[] polarDiagram = { 0, 327, 638, 916, 1145, 1312, 1429, 1528, 1664, 1747, 1801, 1829, 1856, 1832, 1723, 1406, 1205, 1075, 947 };
    float polarDiagramMaxValue = 1856;

    public float minSpeed = 0.0f;
    public float maxSpeed = 1.0f;

    float movementFactor;

    void Update()
    {
        float boatRoatation = transform.eulerAngles.y;

        normalizedBoatDirection = NormalizeAngle(boatRoatation);
        float normalizedWindDirection = NormalizeAngle(windDirection);

        float angleDifference = NormalizeAngle(normalizedBoatDirection - normalizedWindDirection);

        verticalInput = Input.GetAxis("Vertical");

        currentAngle += verticalInput * 60f * Time.deltaTime;

        // Clamp the angle between minAngle and maxAngle
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);

        if (angleDifference > 180f)
        {
            // Starboard Side
            // Rotate around the mast at the clamped angle
            BoomPivot.transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }
        else
        {
            // Port Side
            // Rotate around the mast at the clamped angle
            BoomPivot.transform.localRotation = Quaternion.Euler(0, -currentAngle, 0);
        }

        Speed();
    }

    // Function to normalize angles to the range of 0-360
    private float NormalizeAngle(float angle)
    {
        return Mathf.Repeat(angle, 360f);
    }

    private void Speed()
    {
        float trimFactor = 0;
        float polarSpeedFactor = 0;
        Debug.Log("Boat Direction: " + normalizedBoatDirection);

        float heading = Mathf.Abs(normalizedBoatDirection - windDirection);

        if (normalizedBoatDirection > 20)
        {
            trimFactor = Mathf.Abs((180 - currentAngle - heading) / 180);
        }
        polarSpeedFactor = polarDiagram[(int)heading / 10] / polarDiagramMaxValue;
        float boatSpeed = (minSpeed + ((maxSpeed - minSpeed) * polarSpeedFactor * trimFactor));

        Debug.Log("Heading: " + heading + " | PolarFactor: " + polarSpeedFactor + " | Speed:" + boatSpeed + " | BoomAngle: " + currentAngle + " | TrimFactor: " + trimFactor);

        movementFactor = Mathf.Lerp(movementFactor, boatSpeed, Time.deltaTime / 8);
        //transform.Translate((-boatSpeed * movementFactor) * Time.deltaTime, 0.0f, 0.0f);
    }
}
