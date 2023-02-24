using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialMediaButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openDiscordServerInvite()
    {
        Application.OpenURL("https://discord.gg/E5WtDC8y7T");
    }
    public void openYoutubeChannel()
    {
        Application.OpenURL("https://www.youtube.com/@limeinc2699");
    }
    public void openPatreonPage()
    {
        Application.OpenURL("https://www.patreon.com/lime_inc");
    }

}
