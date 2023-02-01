using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardItem : MonoBehaviour
{
    public TMP_Text usernameTxt, killsTxt, deathTxt;

    public void Initialize (Player player)
    {
        usernameTxt.text = player.NickName;
    }
}
