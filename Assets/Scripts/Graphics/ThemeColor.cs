using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class ThemeColor : MonoBehaviour
{

    public Color color;
    public Material chipEditBackground;
    public Material chipEditBounds;
    public Material chipPackage;
    public Material ioBar;
    
    [Header("Sliders")]
    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        rSlider.value = PlayerPrefs.GetFloat("r");
        gSlider.value = PlayerPrefs.GetFloat("g");
        bSlider.value = PlayerPrefs.GetFloat("b"); 
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Main Camera").GetComponent<Camera>().backgroundColor = color;
        
        Color chipEditBackgroundColor = new Color();
        Color chipEditBoundsColor = new Color();
        Color chipPackageColor = new Color();
        Color ioBarColor = new Color();

        chipEditBackgroundColor.r = color.r - 0.009434f;
        chipEditBackgroundColor.g = color.g - 0.009434f;
        chipEditBackgroundColor.b = color.b - 0.009434f;
        //chipEditBackgroundColor.a = 1;

        chipEditBoundsColor.r = color.r + 0.0660377f;
        chipEditBoundsColor.g = color.g + 0.0660377f;
        chipEditBoundsColor.b = color.b + 0.0660377f;
        //chipEditBoundsColor.a = 1;

        chipPackageColor.r = color.r - 0.018868f;
        chipPackageColor.g = color.g - 0.018868f;
        chipPackageColor.b = color.b - 0.018868f;
        //chipEditBoundsColor.a = 1;

        ioBarColor.r = color.r + 0.0155472f;
        ioBarColor.g = color.g + 0.0155472f;
        ioBarColor.b = color.b + 0.0155472f;
        //ioBar.a = 1;

        chipEditBackground.color = chipEditBackgroundColor;
        chipEditBounds.color = chipEditBoundsColor;
        chipPackage.color = chipPackageColor;
        ioBar.color = ioBarColor;

        if (rSlider != null && gSlider != null && bSlider != null)
        {
            color.r = rSlider.value;
            color.g = gSlider.value;
            color.b = bSlider.value;
        }
    }
    
    public void SaveColor()
    {
        PlayerPrefs.SetFloat("r", rSlider.value);
        PlayerPrefs.SetFloat("g", gSlider.value);
        PlayerPrefs.SetFloat("b", bSlider.value);
    }

    public void Reset()
    {
        rSlider.value = 0.1792453f;
        gSlider.value = 0.1792453f;
        bSlider.value = 0.1792453f;

        SaveColor();
    }
}
