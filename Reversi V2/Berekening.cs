using System.Drawing;
using static System.Math;

namespace Reversi_V2
{
    class Berekening
    {
        private Formule F = new Formule();
        //
        //dit neemt de x en y coordinaten en checkt of het correct is en geeft dan een int array van het bord terug, het maakt ook gebruik van de helpcheck en de checkinsluiten
        //
        public int[,] klikcalc(int x, int y,int breedtestatus,int hoogtestatus,int beurtstatus,int aistatus,int [,] bordstatus,int [,] check,int [,] bordstatushelp)
        {
            if (helpcheck(hoogtestatus, breedtestatus, x, y, beurtstatus, bordstatus, check))
            {            
                    if (bordstatus[x, y] == 0)
                    {
                        bordstatus = checkinsluiten(x, y, breedtestatus, hoogtestatus, check, bordstatus, beurtstatus);
                        bordstatus[x, y] = beurtstatus + 4;
                    }                                  
            }
            return bordstatus;
        }
        //
        //neemt de kleuren van de spelers, wie eraan de beurt is, de data van de array en geeft een bitmap terug
        //
        public Bitmap setbitmap(Color speler1,Color speler2,int beurtstatus,int breedtestatus,int hoogtestatus,int[,] bordstatus,int helpstatus,int aistatus,int [,] check)
        {     
            Bitmap bord = new Bitmap(600 * (breedtestatus / hoogtestatus) + 1, 601);
            int[,] bordstatushelp = new int[breedtestatus, hoogtestatus];
            bordstatushelp = helpchecker(hoogtestatus, breedtestatus, beurtstatus, bordstatus, check);
            Graphics g = Graphics.FromImage(bord);
            Brush brushspeler1 = new SolidBrush(speler1);
            Brush brushspeler2 = new SolidBrush(speler2);
            for (int i = 0; i <= 600 * (breedtestatus / hoogtestatus); i++)
            {
                for (int j = 0; j <=hoogtestatus; j++)
                {
                    bord.SetPixel(i, j * (600 / hoogtestatus), Color.Black);
                }
            }
            for (int i = 0; i <= 600; i++)
            {
                for (int j = 0; j <= breedtestatus; j++)
                {
                    bord.SetPixel(j * ((600 * (breedtestatus / hoogtestatus)) / breedtestatus), i, Color.Black);
                }
            }
            for (int i = 0; i < breedtestatus; i++)
            {
                for (int j = 0; j < hoogtestatus; j++)
                {
                    if (bordstatus[i, j] == 1)
                    {
                        g.FillEllipse(brushspeler1, cirkelx(i,breedtestatus,hoogtestatus), cirkely(j,hoogtestatus), diametercirkel(breedtestatus, hoogtestatus), diametercirkel(breedtestatus, hoogtestatus));
                    }
                    else if (bordstatus[i, j] == 2)
                    {
                        g.FillEllipse(brushspeler2, cirkelx(i,breedtestatus,hoogtestatus), cirkely(j,hoogtestatus), diametercirkel(breedtestatus,hoogtestatus), diametercirkel(breedtestatus, hoogtestatus));
                    }
                    else if (bordstatushelp[i, j] == 3 && helpstatus == 1&& beurtstatus == 1 && aistatus != 3)
                    {
                        g.FillEllipse(brushspeler1, helpcirkelx(i,breedtestatus,hoogtestatus), helpcirkely(j,breedtestatus,hoogtestatus), diameterhelpcirkel(breedtestatus,hoogtestatus), diameterhelpcirkel(breedtestatus,hoogtestatus));
                    }
                    else if (bordstatushelp[i, j] == 4 && helpstatus == 1 && aistatus == 0 && beurtstatus == 2)
                    {
                        g.FillEllipse(brushspeler2, helpcirkelx(i,breedtestatus,hoogtestatus), helpcirkely(j,breedtestatus,hoogtestatus), diameterhelpcirkel(breedtestatus,hoogtestatus), diameterhelpcirkel(breedtestatus,hoogtestatus));
                    }
                    else if (bordstatus[i, j] == 5)
                    {
                        g.FillEllipse(brushspeler1, cirkelx(i, breedtestatus, hoogtestatus), cirkely(j, hoogtestatus), diametercirkel(breedtestatus, hoogtestatus), diametercirkel(breedtestatus, hoogtestatus));
                        g.FillEllipse(Brushes.White, helpcirkelx(i, breedtestatus, hoogtestatus), helpcirkely(j, breedtestatus, hoogtestatus), diameterhelpcirkel(breedtestatus, hoogtestatus), diameterhelpcirkel(breedtestatus, hoogtestatus));
                        bordstatus[i, j] = 1 ;
                    }
                    else if (bordstatus[i, j] == 6)
                    {
                        g.FillEllipse(brushspeler2, cirkelx(i, breedtestatus, hoogtestatus), cirkely(j, hoogtestatus), diametercirkel(breedtestatus, hoogtestatus), diametercirkel(breedtestatus, hoogtestatus));
                        g.FillEllipse(Brushes.White, helpcirkelx(i, breedtestatus, hoogtestatus), helpcirkely(j, breedtestatus, hoogtestatus), diameterhelpcirkel(breedtestatus, hoogtestatus), diameterhelpcirkel(breedtestatus, hoogtestatus));
                        bordstatus[i, j] = 2;
                    }
                }
            }
            return bord;
        }
        //
        //een aantal berekeningen voor het tekenen van de cirkels op de bitmap
        //
        public int cirkelx(int i,int breedtestatus,int hoogtestatus) { return i * 600 * (breedtestatus / hoogtestatus) / breedtestatus; }
        public int cirkely(int i, int hoogtestatus) { return i * 600 / hoogtestatus; }
        public int helpcirkelx(int i, int breedtestatus, int hoogtestatus) { return i * 600 * (breedtestatus / hoogtestatus) / breedtestatus + 150 * (breedtestatus / hoogtestatus) / breedtestatus; }
        public int helpcirkely(int i, int breedtestatus, int hoogtestatus) { return i * 600 / hoogtestatus + 150 / hoogtestatus; }
        public int diametercirkel(int breedtestatus, int hoogtestatus) { return 600 * (breedtestatus / hoogtestatus) / breedtestatus; }
        public int diameterhelpcirkel( int breedtestatus, int hoogtestatus) { return 300 * (breedtestatus / hoogtestatus) / breedtestatus; }
        //
        //berekent het aantal van beurt in de array bord
        //
        public int berekenaantal(int beurt,int[,] bord)
        {
            int aantal = 0;
            foreach (int i in bord)
            {
                if (i == beurt)
                {
                    aantal++;
                }
            }
            return aantal;
        }
        //
        //dit neemt de x en y coordinaat en de int array van het bord en sluit de stenen in
        //
        public int[,] checkinsluiten(int x, int y,int breedtestatus,int hoogtestatus,int [,] check,int [,] bordstatus,int beurtstatus)
        {
            int teller;
            for (int i = 0; i < 8; i++)
            {
                for (teller = 2; teller < Max(breedtestatus, hoogtestatus); teller++)
                {
                    if (F.formule1(teller, x, y, i, breedtestatus, hoogtestatus,check))
                    {
                        if (bordstatus[x + check[0,i] * teller, y + check[1,i] * teller] == beurtstatus && F.formule2(teller - 1, x, y, i, bordstatus, beurtstatus,check))
                        {                         
                            for (int j = 1; j <= teller - 1; j++)
                            {
                                bordstatus[x + check[0,i] * j, y + check[1,i] * j] = beurtstatus;
                            }
                        }
                    }
                }
            }
            return bordstatus;
        }
        //
        //geeft de winstatus terug, met het gegeven aantal zetten van de twee spelers die ze kunnen zetten
        //
        public int winnaar(int [,] bordstatus,int hoogtestatus,int breedtestatus,int [,] check,int aantal1,int aantal2)
        {
            int winstatus = 0;           
            if (aantal1==0 &&aantal2 ==0)
            {
                if (berekenaantal(1, bordstatus) > berekenaantal(2,bordstatus))
                {
                    winstatus = 1;
                }
                if (berekenaantal(2,bordstatus)> berekenaantal(1,bordstatus))
                {
                    winstatus = 2;
                }
                if (berekenaantal(2,bordstatus)==berekenaantal(1,bordstatus))
                {
                    winstatus = 3;
                }
            }
            return winstatus;
        }
        //
        //Helpcheck neemt een int array en de x en y coordinaat en geeft een bool terug die aangeeft of degene die aan de beurt was daar kan neerzetten
        //
        public bool helpcheck(int hoogtestatus,int breedtestatus,int x,int y,int beurtstatus,int[,] bordstatus,int[,] check)
        {
            if (bordstatus[x, y] == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    int teller = 2;
                    if (F.formule1(teller, x, y, i, breedtestatus, hoogtestatus, check))
                    {
                        if (bordstatus[x + check[0, i], y + check[1, i]] != beurtstatus && bordstatus[x + check[0, i], y + check[1, i]] != 0)
                        {
                            for (teller = 2; teller < Max(breedtestatus, hoogtestatus); teller++)
                            {
                                if (F.formule1(teller, x, y, i, breedtestatus, hoogtestatus, check))
                                {
                                    if (bordstatus[x + check[0, i] * teller, y + check[1, i] * teller] == beurtstatus && F.formule2(teller - 1, x, y, i, bordstatus, beurtstatus, check))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }  
        //
        //neemt een array van een bord en geeft een array van de helpcheck waardes
        //      
        public int[,] helpchecker(int hoogtestatus, int breedtestatus, int beurtstatus, int[,] bordstatus, int[,] check)
        {
            int[,] bordstatushelp = new int[breedtestatus, hoogtestatus];
            for (int i = 0; i < breedtestatus; i++)
            {
                for (int j = 0; j < hoogtestatus; j++)
                {
                    bordstatushelp[i, j] = 0;
                }
            }
            for (int x = 0; x < breedtestatus; x++)
            {
                for (int y = 0; y < hoogtestatus; y++)
                {
                    if (helpcheck(hoogtestatus, breedtestatus, x, y, beurtstatus, bordstatus, check))
                    {
                        bordstatushelp[x, y] = beurtstatus + 2;
                    }
                }
            }
            return bordstatushelp;
        }
    }
}
