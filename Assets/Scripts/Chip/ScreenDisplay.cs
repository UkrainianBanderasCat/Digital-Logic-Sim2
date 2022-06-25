using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplay : BuiltinChip
{
    public Image[] pixels;

    protected override void ProcessOutput (int[] input) {
        bool store = input[input.Length - 1] == 1;
        if (!store) {
            return;
        }

        float red = input[input.Length - 4];
        float green = input[input.Length - 3];
        float blue = input[input.Length - 2];

        float address = 0;
        for (int i = 0; i < input.Length - 4; i++) {
            if (input[i] == 1) {
                address += Mathf.Pow(2f, (float)i);
            }
        }
        if (address > pixels.Length) {
            return;
        }

        Color color = new Color(red, green, blue);
        pixels[(int)address].color = color;
    }
}
