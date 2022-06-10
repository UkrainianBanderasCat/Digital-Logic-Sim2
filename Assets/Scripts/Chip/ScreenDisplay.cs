using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplay : BuiltinChip
{
    public Color onColor;
    public Color offColor;
    public Image[] pixels;

    protected override void ProcessOutput (int[] input) {
        bool store = input[input.Length - 1] == 1;
        if (!store) {
            return;
        }

        bool data = input[input.Length - 2] == 1;
        float address = 0;
        for (int i = 0; i < input.Length - 2; i++) {
            if (input[i] == 1) {
                address += Mathf.Pow(2f, (float)i);
            }
        }
        if (address > pixels.Length) {
            return;
        }

        Color color = data ? onColor : offColor;
        pixels[(int)address].color = color;
    }
}
