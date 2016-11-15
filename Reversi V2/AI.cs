
using System;

namespace Reversi_V2
{
    class AI
    {
        private Berekening B = new Berekening();
        //
        //een ai die een bordstatushelp array aanmaakt en die afgaat. het eerste punt dat die tegen komt wordt gekozen
        //
        public int[] aidumb(int aistatus, int beurtstatus, int breedtestatus, int hoogtestatus, int[,] bordstatus, int[,] check)
        {
            int[] xy = new int[2] { 0, 0 };
            int[,] bordstatushelp = new int[breedtestatus, hoogtestatus];
            bordstatushelp = B.helpchecker(hoogtestatus, breedtestatus, beurtstatus, bordstatus, check);
            int aihelp = 0;
            //dumb ai
            if (aistatus == 1 && B.berekenaantal(beurtstatus + 2, bordstatushelp) != 0)
            {
                for (int i = 0; i < breedtestatus && aihelp == 0; i++)
                {
                    for (int j = 0; j < hoogtestatus && aihelp == 0; j++)
                    {
                        if (bordstatushelp[i, j] == beurtstatus + 2)
                        {
                            aihelp = 1;
                            xy[0] = i;
                            xy[1] = j;
                            return xy;
                        }
                    }
                }
            }
            return xy;
        }
        //
        //een ai die een bord maakt waar bij elk punt waar hij kan neerzetten een waarde wordt gezet die de meeste punten opleveren
        //dan gaat hij die array af en het eerste punt dat het hoogste is neemt hij
        public int[] aismart(int aistatus, int beurtstatus, int breedtestatus, int hoogtestatus, int[,] bordstatus, int[,] check)
        {
            int[] xy = new int[2] { 0, 0 };
            int[,] bordstatusai = new int[breedtestatus, hoogtestatus];
            int[,] bordstatusaihelp = new int[breedtestatus, hoogtestatus];
            int[,] bordstatushelp = new int[breedtestatus, hoogtestatus];
            bordstatushelp = B.helpchecker(hoogtestatus, breedtestatus, beurtstatus, bordstatus, check);
            int aihelp = 0;
            for (int i = 0; i < breedtestatus; i++)
            {
                for (int j = 0; j < hoogtestatus; j++)
                {
                    bordstatusaihelp[i, j] = 0;
                }
            }
            // "smart" ai
            if (B.berekenaantal(beurtstatus + 2, bordstatushelp) != 0)
            {
                for (int x = 0; x < breedtestatus; x++)
                {
                    for (int y = 0; y < hoogtestatus; y++)
                    {
                        if (bordstatushelp[x, y] == beurtstatus + 2)
                        {
                            for (int i = 0; i < breedtestatus; i++)
                            {
                                for (int j = 0; j < hoogtestatus; j++)
                                {
                                    bordstatusai[i, j] = bordstatus[i, j];
                                }
                            }
                            bordstatusai = B.klikcalc(x, y, breedtestatus, hoogtestatus, beurtstatus, aistatus, bordstatusai, check, bordstatusaihelp);
                            int aantal = B.berekenaantal(beurtstatus, bordstatusai) - B.berekenaantal(beurtstatus, bordstatus);
                            bordstatusaihelp[x, y] = aantal;
                        }
                    }
                }
                for (int z = 20; z > 0 && aihelp == 0; z--)
                {
                    for (int i = 0; i < breedtestatus && aihelp == 0; i++)
                    {
                        for (int j = 0; j < hoogtestatus && aihelp == 0; j++)
                        {
                            if (bordstatusaihelp[i, j] == z & aihelp == 0)
                            {
                                xy[0] = i;
                                xy[1] = j;
                                return xy;                               
                            }
                        }
                    }
                }
            }
            return xy;
        }
        //
        //een ai die hetzelfde werkt als de smart ai maar hij neemt een random coordinaat die de meeste punten opleverd
        //
       public int[] aismartrandom(int beurtstatus,int beurttijd, int aistatus,int breedtestatus,int hoogtestatus,int[,] bordstatus,int [,] check,Random randomaantal)
        {
                    
            int[] xy = new int[2] { 0, 0 };
            int[,] bordstatusai = new int[breedtestatus, hoogtestatus];
            int[,] bordstatushelp = new int[breedtestatus, hoogtestatus];
            bordstatushelp = B.helpchecker(hoogtestatus, breedtestatus, beurtstatus, bordstatus, check);      
            int aihelp = 0;
            int[,,] bordstatusaihelp = new int[breedtestatus * hoogtestatus, 2, breedtestatus * hoogtestatus];
            int[] aantalvan = new int[breedtestatus * hoogtestatus];
            int rand;
            for (int i = 0; i < breedtestatus * hoogtestatus; i++)
            {
                aantalvan[i] = -1;
            }
            for (int i = 0; i < breedtestatus * hoogtestatus; i++)
            {
                for (int j = 0; j <= 1; j++)
                {
                    for (int n = 0; n < breedtestatus * hoogtestatus; n++)
                    {
                        bordstatusaihelp[i, j, n] = 0;
                    }
                }
            }
            // random "smart" ai
            if (B.berekenaantal(beurtstatus + 2, bordstatushelp) != 0)
            {
                for (int x = 0; x < breedtestatus; x++)
                {
                    for (int y = 0; y < hoogtestatus; y++)
                    {
                        if (bordstatushelp[x, y] == beurtstatus + 2)
                        {
                            for (int i = 0; i < breedtestatus; i++)
                            {
                                for (int j = 0; j < hoogtestatus; j++)
                                {
                                    bordstatusai[i, j] = bordstatus[i, j];
                                }
                            }
                            bordstatusai[x, y] = beurtstatus;
                            bordstatusai = B.checkinsluiten(x, y,breedtestatus,hoogtestatus,check,bordstatusai,beurtstatus);
                            int aantal = B.berekenaantal(2, bordstatusai) - B.berekenaantal(2, bordstatus);
                            if (aantal < 0)
                            {
                                break;
                            }
                            aantalvan[aantal]++;
                            bordstatusaihelp[aantal, 0, aantalvan[aantal]] = x;
                            bordstatusaihelp[aantal, 1, aantalvan[aantal]] = y;
                        }
                    }
                }
                for (int i = beurtstatus * hoogtestatus; i > 0 && aihelp == 0; i--)
                {
                    if (aantalvan[i] >= 0)
                    {
                        rand = randomaantal.Next(0, aantalvan[i] + 1);                       
                        xy[0] = bordstatusaihelp[i, 0, rand];
                        xy[1] = bordstatusaihelp[i, 1, rand];

                        aihelp = 1;
                        return xy;
                    }
                }
            }
            return xy;
        }
    }
}
