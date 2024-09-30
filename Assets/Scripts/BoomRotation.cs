using Unity.VisualScripting;
using UnityEngine;

public class BoomRotation : MonoBehaviour
{
    public Transform BoomPivot; // Reference to the mast (or boom pivot)
    public float rotationSpeed = 50f; // Speed of rotation

    // Angle limits
    public float minAngle = 0f;
    public float maxAngle = 90f;

    public float degreesToWind = 40f;

    private float currentAngle = 0f; // Current rotation angle

    float verticalInput;

    public float windDirection = 90.0f;
    public float windSpeed = 1.0f;

    float timeFacingWind = 0.0f; // Time spent facing directly into the wind
    public float maxTimeFacingWind = 3.0f; // Time before coming to a standstill

    float normalizedRelativeBoatDirection;

    float relativeBoatDirection; // Boat's direction relative to the wind

    int[] polarDiagram = { 327, 327, 638, 916, 1145, 1312, 1429, 1528, 1664, 1747, 1801, 1829, 1856, 1832, 1723, 1406, 1205, 1075, 947 };
    float polarDiagramMaxValue = 1856;

    float trimFactor = 0;
    float polarSpeedFactor = 0;
    float previousPolarSpeedFactor = 0;
    float newPolarSpeedFactor = 0;

    public float minSpeed = 0.0f;
    public float maxSpeed = 1.0f;

    float movementFactor;
    bool isStalled = false;


    bool isInWater = false;



    private void OnTriggerEnter(Collider other)
    {
        isInWater = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isInWater = false;
    }

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
        float heading = Mathf.Abs(normalizedRelativeBoatDirection);

        if (heading <= degreesToWind)
        {

            // Increment the time the boat is facing into the wind
            timeFacingWind += Time.deltaTime;

            // Gradually decrease speed over 3 seconds (maxTimeFacingWind)
            float slowdownFactor = Mathf.Clamp01(1.0f - (timeFacingWind / maxTimeFacingWind));


            // Calculate trimFactor based on how long the boat is facing the wind
            trimFactor = slowdownFactor;

            // If the boat remains in the no-sail zone for longer than 3 seconds, it will stop
            if (timeFacingWind >= maxTimeFacingWind)
            {
                isStalled = true;
            }

        }
        else
        {
            timeFacingWind = 0.0f;
            isStalled = false;

            //trimFactor = Mathf.Abs((180 - Mathf.Abs(2 * currentAngle - heading)) / 180);
            
            
            trimFactor = Mathf.Abs((140 - Mathf.Abs(1.55f * currentAngle - (heading - degreesToWind))) / 140);


            // Use the polar diagram to adjust speed based on heading
            newPolarSpeedFactor = polarDiagram[Mathf.Clamp((int)heading / 10, 0, polarDiagram.Length - 1)] / polarDiagramMaxValue;
        }

        previousPolarSpeedFactor = polarSpeedFactor;
        polarSpeedFactor = Mathf.Lerp(previousPolarSpeedFactor, newPolarSpeedFactor, Time.deltaTime / 2f);

        // Calculate boat speed
        float boatSpeed = (maxSpeed - minSpeed) * polarSpeedFactor * trimFactor + minSpeed;

        boatSpeed = Mathf.Clamp(boatSpeed, minSpeed, maxSpeed);
        movementFactor = Mathf.Lerp(movementFactor, boatSpeed, Time.deltaTime * 2f);


        if (isStalled)
        {
            boatSpeed = 0.0f; // If stalled, stop the boat
        }

        Debug.Log("Heading: " + heading + " | PolarFactor: " + polarSpeedFactor + " | Speed:" + boatSpeed + " | BoomAngle: " + currentAngle + " | TrimFactor: " + trimFactor);


        if (isInWater)
        {
            transform.Translate(-movementFactor * Time.deltaTime, 0.0f, 0.0f);
        }
    }
}
