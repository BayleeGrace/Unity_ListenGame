using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class SoundPointer : MonoBehaviour
{
    public Pawn pawn;
    public Transform targetTransform; // Variable that the arrow will be pointing at in reference to the screen
    private RectTransform pointerRectTransform; // Variable to store the rectangular transform of the arrow
    [SerializeField] private GameObject pointerPrefab;

    public void Start()
    {
        targetTransform = GameManager.instance.humanPlayer.transform;
        GameObject newPointer = Instantiate(pointerPrefab, this.transform);
        // TODO: Find all SOUNDS in the scene and set their transforms to targetTransforms
    }

    public void Update()
    {
            // TODO: If the yokai can hear the target's noisemaker...

            // Create a pointer prefab and instantiate it for each SOUND in the scene
            

            // Grab the transform of the UI pointer (first cnhild of the canvas!)
            pointerRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
            // Find the transform to point towards (OBJECT IN WORLD SPACE)
            Vector3 toTransform = targetTransform.position;
            // Reference the current transform
            Vector3 fromTransform = pawn.transform.position;
            // Create
            Vector3 newPosition = (toTransform - fromTransform);

            // Get the angle in which the arrow should be facing
            float angle = UtilsClass.GetAngleFromVectorFloat(newPosition);
            pointerRectTransform.localEulerAngles = new Vector3(0,0, angle);

            // Test if the target is on the screen
            Vector3 targetTransformScreenPoint = Camera.main.WorldToScreenPoint(targetTransform.position);
            Controller yokaiController = GameManager.instance.yokaiPlayer.GetComponent<Controller>();
            bool isOffScreen = targetTransformScreenPoint.x <= 0 || targetTransformScreenPoint.x >= Screen.width || targetTransformScreenPoint.y <= 0 || targetTransformScreenPoint.y >= Screen.height;
            Debug.Log(isOffScreen + " " + targetTransform.position);

            // If the target transform is off the screen...
            if (isOffScreen == true)
            {
                Vector3 cappedTargetScreenPosition = targetTransformScreenPoint;
                if (cappedTargetScreenPosition.x <= 0) cappedTargetScreenPosition.x = 0f;
                if (cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = Screen.width;
                if (cappedTargetScreenPosition.y <= 0) cappedTargetScreenPosition.y = 0f;
                if (cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = Screen.height;

                Vector3 pointerWorldPosition = Camera.main.ScreenToWorldPoint(cappedTargetScreenPosition);
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, 0f, pointerRectTransform.localPosition.z);
            }

    }
}
