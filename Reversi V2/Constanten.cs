using System.Drawing;

namespace Reversi_V2
{
    //
    //de class waar alle constanten waardes opgeslagen staan
    //
    public class Constanten
    {        
        public readonly string[] benaming =  { "Rood", "Paars", "Roze", "Magenta", "Voilet", "Blauw", "Turquoise", "SteelBlue", "Cyaan", "Geel", "Groen", "PaleGreen", "LawnGreen" };
        public readonly Color[] kleuren = new Color[] { Color.Red, Color.Purple, Color.Pink, Color.Magenta, Color.Violet, Color.Blue, Color.Turquoise, Color.SteelBlue, Color.Cyan, Color.Yellow, Color.Green, Color.PaleGreen, Color.LawnGreen };
        public readonly string[] beurttijdbenaming = { "1ms", "50 ms", "100 ms", "200 ms", "400 ms", "800 ms" };
        public readonly int[] beurttijdint = {1, 50, 100, 200, 400, 800 };
        public readonly string[] grootte = { "6x6", "12x6", "8x8","16x8", "10x10", "20x10", "12x12","24x12", "20x20", "40x20","80x40" };
        public readonly int[,] sizestatus = { { 6, 12, 8,16, 10, 20, 12,24, 20, 40,80}, { 6, 6, 8,8, 10, 10, 12,12, 20, 20,40 } };
        public readonly int[,] check = { { -1, -1, 0, 1, 1, 1, 0, -1 }, { 0, -1, -1, -1, 0, 1, 1, 1 } };
        public readonly string[] AIbenaming = { "uit", "Dumb", "Smart","AI vs AI","SmartRandom" };
    }
}
