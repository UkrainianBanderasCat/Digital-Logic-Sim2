using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WireColorPicker : MonoBehaviour
{

	public static Palette NextWireColorPalette;

	public Palette StartPalette;
	public Button menuOpenButton;
	public GameObject menuHolder;
	public Image chipNameField;
	public Image previewImage;
	public Button doneButton;
	public Slider hueSlider;
	public Slider saturationSlider;
	public Slider valueSlider;

	public Color color;

    private void Start()
    {
		doneButton.onClick.AddListener(FinishCreation);
		menuOpenButton.onClick.AddListener(OpenMenu);

		hueSlider.onValueChanged.AddListener(ColourSliderChanged);
		saturationSlider.onValueChanged.AddListener(ColourSliderChanged);
		valueSlider.onValueChanged.AddListener(ColourSliderChanged);

		NextWireColorPalette = ScriptableObject.CreateInstance<Palette>();
		NextWireColorPalette.offCol = StartPalette.offCol;
		NextWireColorPalette.onCol = StartPalette.onCol;
		NextWireColorPalette.highZCol = StartPalette.highZCol;
		NextWireColorPalette.nonInteractableCol = StartPalette.nonInteractableCol;

		Color.RGBToHSV(StartPalette.offCol, out float hueValue, out float satValue, out float valValue);

        hueSlider.value = hueValue;
		saturationSlider.value = satValue;
		valueSlider.value = valValue;
	}

	void ColourSliderChanged(float sliderValue)
	{
		Color chipCol = Color.HSVToRGB(hueSlider.value, saturationSlider.value, valueSlider.value);
		UpdateColour(chipCol);

	}

	void OpenMenu()
	{
		menuHolder.SetActive(true);
	}

	public void FinishCreation()
	{
		NextWireColorPalette = ScriptableObject.CreateInstance<Palette>();
		NextWireColorPalette.offCol = color;
		NextWireColorPalette.onCol = StartPalette.onCol;
		NextWireColorPalette.nonInteractableCol = new Color(63,63,63);
		NextWireColorPalette.highZCol = new Color(0, 0, 0, 0);
	}

	void UpdateColour(Color chipCol)
	{
		chipNameField.color = chipCol;
		previewImage.color = chipCol;
		color = chipCol;

	}

}
