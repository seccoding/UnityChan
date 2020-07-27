using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooltime
{
    public static IEnumerator CooltimeCoroutine(Image skillImage, float cooltime)
    {
        float time = 0;
        while (true)
        {
            time += 0.02f;
            skillImage.fillAmount = time / cooltime;
            if (time >= cooltime)
                break;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
