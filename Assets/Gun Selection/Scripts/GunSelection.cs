using UnityEngine;

public class GunSelection : MonoBehaviour
{
    public int currentGunIndex;
    
    [SerializeField] private GameObject[] guns;
    [SerializeField] private Animator[] gunsUI;

    public void Up()
    {
        if (currentGunIndex > 0)
        {
            gunsUI[currentGunIndex].Play("DeselectUp", -1, 0f);
            gunsUI[currentGunIndex - 1].Play("SelectUp", -1, 0f);
            currentGunIndex--;
        }
        else
        {
            gunsUI[currentGunIndex].Play("DeselectUp", -1, 0f);
            gunsUI[guns.Length - 1].Play("SelectUp", -1, 0f);
            currentGunIndex = guns.Length - 1;
        }
        
        UpdateGun();
    }

    public void Down()
    {
        if (currentGunIndex < guns.Length - 1)
        {
            gunsUI[currentGunIndex].Play("DeselectDown", -1, 0f);
            gunsUI[currentGunIndex + 1].Play("SelectDown", -1, 0f);
            currentGunIndex++;
        }
        else
        {
            gunsUI[currentGunIndex].Play("DeselectDown", -1, 0f);
            gunsUI[0].Play("SelectDown", -1, 0f);
            currentGunIndex = 0;
        }
        
        UpdateGun();
    }

    private void UpdateGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (i == currentGunIndex)
                guns[i].SetActive(true);
            else
                guns[i].SetActive(false);
        }
    }
}
