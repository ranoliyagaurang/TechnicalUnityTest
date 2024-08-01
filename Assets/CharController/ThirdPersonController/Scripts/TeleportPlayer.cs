using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TeleportPlayer : MonoBehaviour
{
    private NavigateCharacter navigateCharacterRef;
    public List<Button> buttons;
    public List<Vector3> locations;
    public List<Quaternion> rotations;
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;
            buttons[index].onClick.RemoveAllListeners();
            buttons[index].onClick.AddListener(() => GoToDestination(locations[index], rotations[index]));
        }
        navigateCharacterRef = GetComponentInChildren<NavigateCharacter>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void GoToDestination(Vector3 destinationVector3, Quaternion destinationRotation)
    {
        navigateCharacterRef.enabled = false;
        Debug.Log("button clicked");
        transform.position = destinationVector3;
        transform.rotation = destinationRotation;
        StartCoroutine(LateEnableNavCharacter(0.2f));
    }
    public IEnumerator LateEnableNavCharacter(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        navigateCharacterRef.enabled = true;
    }
}
