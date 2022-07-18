using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDisplay : BuiltinChip
{
    [SerializeField]
    private int width = 12;
    [SerializeField]
    private int height = 8;
    [SerializeField]
    private Image[] pixels;

    protected override void ProcessOutput (int[] input) {
        bool clear = input[11] == 1;
        if (clear) {
            ClearScreen();
            return;
        }
        bool store = input[10] == 1;
        if (!store) {
            return;
        }

        int red = input[7];
        int green = input[8];
        int blue = input[9];    

        int xCord = BinaryToDecimal(input.Take(4));
        if (xCord >= width) {
            return;
        }
        int yCord = BinaryToDecimal(input.Skip(4).Take(3));
        int index = width * (height - yCord - 1) + xCord;

        Color color = new Color(red, green, blue);
        pixels[index].color = color;
    }

    private int BinaryToDecimal(IEnumerable<int> bits) {
        float number = 0;
        int i = 0;
        foreach (int bit in bits) {
            if (bit == 1) {
                number += Mathf.Pow(2, i);
            }
            i++;
        }
        return (int)number;
    }

    private void ClearScreen() {
        Color blankColor = Color.white;
        foreach (Image pixel in pixels) {
            pixel.color = blankColor;
        }
    }
}
