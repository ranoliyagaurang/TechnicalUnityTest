using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IntroScript : MonoBehaviour
{
    [SerializeField] ThirdPersonController thirdPersonController;
    [SerializeField] NavigateCharacter navigateCharacter;
    [SerializeField] StarterAssetsInputs starterAssetsInputs;
    [SerializeField] PlayerInput playerInput;
    public List<GameObject> buttonList;
    private void Awake()
    {
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].SetActive(false);
        }
        navigateCharacter.enabled = false;
        starterAssetsInputs.enabled = false;
        thirdPersonController.enabled = false;
        playerInput.enabled = false;
    }

    public void IntroOver()
    {
        StartCoroutine(LateEnableScripts(4f));
    }

    public IEnumerator LateEnableScripts(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].SetActive(true);
        }
        navigateCharacter.enabled = true;
        starterAssetsInputs.enabled = true;
        thirdPersonController.enabled = true;
        playerInput.enabled = true;
    }
}
