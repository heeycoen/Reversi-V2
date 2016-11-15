using System.Drawing;

namespace Reversi_V2
{
    //
    //de classe die alle spelervariabelen en bordvariabelen onthouden
    //
    class spelervariabelen
    {
        private string naam;
        private Color kleur;
        private int score, winscore;

        public string Naam
        {
            set
            {
                naam = value;
            }
            get
            {
                return naam;
            }
        }
        public Color Kleur
        {
            set
            {
                kleur = value;
            }
            get
            {
                return kleur;
            }
        }
        public int Score
        {
            set
            {
                score = value;
            }
            get
            {
                return score;
            }
        }
        public int Winscore
        {
            set
            {
                winscore = value;
            }
            get
            {
                return winscore;
            }
        }
    }
    class statusvariabelen
    {
        private int breedtestatus, hoogtestatus, beurtstatus, helpstatus, aistatus, winstatus, beurttijdstatus;

        public int Breedte
        {
            set
            {
                breedtestatus = value;
            }
            get
            {
                return breedtestatus;
            }
        }
        public int Hoogte
        {
            set
            {
                hoogtestatus = value;
            }
            get
            {
                return hoogtestatus;
            }
        }
        public int Beurt
        {
            set
            {
                beurtstatus = value;
            }
            get
            {
                return beurtstatus;
            }
        }
        public int Help
        {
            set
            {
                helpstatus = value;
            }
            get
            {
                return helpstatus;
            }
        }
        public int Aistat
        {
            set
            {
                aistatus = value;
            }
            get
            {
                return aistatus;
            }
        }
        public int Win
        {
            set
            {
                winstatus = value;
            }
            get
            {
                return winstatus;
            }
        }
        public int Beurttijd
        {
            set
            {
                beurttijdstatus = value;
            }
            get
            {
                return beurttijdstatus;
            }
        }
    }
}
