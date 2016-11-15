using System.Windows.Forms;

namespace Reversi_V2
{
    static class Program
    {
        [System.STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            
            reversi form;
            form = new reversi();
            Application.Run(form);
        }
    }
}
