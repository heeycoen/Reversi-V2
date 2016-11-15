using System;
using System.Drawing;
using System.Windows.Forms;


namespace Reversi_V2
{
    class OptiesForm : Form
    {
        private int aistatus, beurttijdstatus,speler1,speler2;
        private string naamspeler1, naamspeler2;
        private Constanten C = new Constanten();
        private string[] Config, Config1;
        public string[] config
        {
            get
            {
                return Config;
            }
            set
            {
                Config = value;
                Config1 = Config;
                speler1 = int.Parse(Config[0]);
                speler2 = int.Parse(Config[1]);
                beurttijdstatus = int.Parse(Config[3]);
                aistatus = int.Parse(Config[2]);
                naamspeler1 = Config[4];
                naamspeler2 = Config[5];
                AIbeurtstat.SelectedIndex = beurttijdstatus;
                AIstat.SelectedIndex = aistatus;
                Kleurspel1.SelectedIndex = speler1;
                Kleurspel2.SelectedIndex = speler2;
                naam2.Text = naamspeler2;
                naam1.Text = naamspeler1;
                if (naamspeler1 != "")
                {
                    spel1.Text = "Speler: " + naamspeler1;
                }
                else
                {
                    spel1.Text = "Speler 1";
                }
                if (naamspeler2 != "")
                {
                    spel2.Text = "Speler: " + naamspeler2;
                }
                else
                {
                    spel2.Text = "Speler 2";
                }
            }
        }
        //
        //checkt of de de andere speler niet dezelfde naam heeft en zet die in de config
        //
        private void naamspeler1changed(object sender,EventArgs e)
        {
            if (naam1.Text != naamspeler2)
            {
                naamspeler1 = naam1.Text;
                spel1.Text = "Speler: " + naamspeler1;
            }
            if (naam1.Text == "")
            {
                naamspeler1 = naam1.Text;
                spel1.Text = "Speler 1";
            }
            Config[4] = naamspeler1;
        }
        //
        //checkt of de de andere speler niet dezelfde naam heeft en zet die in de config
        //
        private void naamspeler2changed(object sender, EventArgs e)
        {
            if (naam2.Text != naamspeler1)
            {
                naamspeler2 = naam2.Text;
                spel2.Text = "Speler: " + naamspeler2;
            }
            if (naam2.Text == "")
            {
                naamspeler2 = naam2.Text;
                spel2.Text = "Speler 2";
            }
            Config[5] = naamspeler2;
        }
        //
        //checkt of de andere speler niet dezelfde kleur heeft en als dat niet zo is zet hij die neer
        //
        private void Kleurspel2_ItemCheck(object sender, EventArgs e)
        {
            if (Kleurspel2.SelectedIndex == Kleurspel1.SelectedIndex)
            {
                Kleurspel2.SelectedIndex = speler2;
            }
            else
            {
                speler2 = Kleurspel2.SelectedIndex;
            }
            Config[1] =  speler2.ToString();
            spel2.BackColor = C.kleuren[Kleurspel2.SelectedIndex];
        }
        //
        //checkt of de andere speler niet dezelfde kleur heeft en als dat niet zo is zet hij die neer
        //
        private void Kleurspel1_ItemCheck(object sender, EventArgs e)
        {         
            if (Kleurspel1.SelectedIndex == Kleurspel2.SelectedIndex)
            {
                Kleurspel1.SelectedIndex = speler1;
            }
            else
            {
                speler1 = Kleurspel1.SelectedIndex;
            }
            Config[0] = speler1.ToString();
            spel1.BackColor = C.kleuren[Kleurspel1.SelectedIndex];
        }
        //
        //zet de aistatus in de config
        //
        private void AIstat_SelectedIndexChanged(object sender, EventArgs e)
        {
            aistatus = AIstat.SelectedIndex;
            Config[2] = aistatus.ToString();    
        }
        //
        //zet de beurttijd in de config
        //
        private void AIbeurtstat_SelectedIndexChanged(object sender, EventArgs e)
        {
            beurttijdstatus = AIbeurtstat.SelectedIndex;
            Config[3] = beurttijdstatus.ToString();          
        }
        private TextBox naam1, naam2;
        private Button Cancel, Apply;
        private Label spel1, spel2, AI1, AI2;
        private ComboBox Kleurspel1,Kleurspel2,AIstat,AIbeurtstat;

        private void InitializeComponent()
        {    
            //
            //
            //
            Cancel = new Button();
            Cancel.Location = new Point(110, 210);
            Cancel.Size = new Size(60, 25);
            Cancel.Text = "Cancel";
            Cancel.Click += cancel;
            //
            //
            //
            Apply = new Button();
            Apply.Location = new Point(10, 210);
            Apply.Size = new Size(60, 25);
            Apply.Text = "Apply";
            Apply.Click += apply;
            // 
            // Kleurspel1
            // 
            naam1 = new TextBox();
            naam1.Location = new Point(160, 5);
            naam1.Size = new Size(125, 20);
            naam1.Text = naamspeler1;
            naam1.TextChanged += naamspeler1changed;
            naam1.MaxLength = 10;


            spel1 = new Label();
            spel1.Location = new Point(10, 10);
            spel1.Size = new Size(145, 15);
            if (naamspeler1 != "")
            {
                spel1.Text = "Speler: " + naamspeler1;
            }
            else
            {
                spel1.Text = "Speler 1";
            }            

            Kleurspel1 = new ComboBox();
            Kleurspel1.FormattingEnabled = true;
            Kleurspel1.Location = new Point(10, 30);
            Kleurspel1.Name = "Kleurspel1";
            Kleurspel1.Size = new Size(280, 20);
            //
            Kleurspel1.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < C.benaming.Length; i++)
            {
                Kleurspel1.Items.Add(C.benaming[i]);
            }
            Kleurspel1.SelectedIndex = speler1;
            Kleurspel1.SelectedIndexChanged += Kleurspel1_ItemCheck;
            spel1.BackColor = C.kleuren[Kleurspel1.SelectedIndex];
            // 
            // Kleurspel2
            // 
            naam2 = new TextBox();
            naam2.Location = new Point(160, 55);
            naam2.Size = new Size(125, 20);
            naam2.Text = naamspeler2;
            naam2.TextChanged += naamspeler2changed;
            naam2.MaxLength = 10;

            spel2 = new Label();
            spel2.Location = new Point(10, 60);
            spel2.Size = new Size(145, 15);
            if (naamspeler2 != "")
            {
                spel2.Text = "Speler: " + naamspeler2; 
            }
            else
            {
                spel2.Text = "Speler 2";
            }           
            Kleurspel2 = new ComboBox();
            Kleurspel2.FormattingEnabled = true;
            Kleurspel2.Location = new Point(10, 80);
            Kleurspel2.Name = "Kleurspel2";
            Kleurspel2.Size = new Size(280, 20);
            //
            Kleurspel2.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < C.benaming.Length; i++)
            {
                Kleurspel2.Items.Add(C.benaming[i]);
            }
            Kleurspel2.SelectedIndex = speler2;
            Kleurspel2.SelectedIndexChanged += Kleurspel2_ItemCheck;
            spel2.BackColor = C.kleuren[Kleurspel2.SelectedIndex];
            // 
            // AIstat
            //            
            AI1 = new Label();
            AI1.Location = new Point(10, 110);
            AI1.Size = new Size(100, 20);
            AI1.Text = "AI status";

            AIstat = new ComboBox();
            AIstat.FormattingEnabled = true;
            AIstat.Location = new Point(10, 130);
            AIstat.Name = "AIstat";
            AIstat.Size = new Size(280, 20);
            //
            AIstat.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < C.AIbenaming.Length; i++)
            {
                AIstat.Items.Add(C.AIbenaming[i]);
            }
            AIstat.SelectedIndex = aistatus;
            AIstat.SelectedIndexChanged += AIstat_SelectedIndexChanged;
            //
            // AI beurtstat
            //
            AI2 = new Label();
            AI2.Location = new Point(10, 160);
            AI2.Size = new Size(100, 20);
            AI2.Text = "AI beurttijd";

            AIbeurtstat = new ComboBox();
            AIbeurtstat.FormattingEnabled = true;
            AIbeurtstat.Location = new Point(10, 185);
            AIbeurtstat.Name = "AIbeurtstat";
            AIbeurtstat.Size = new Size(280, 20);
            //
            AIbeurtstat.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < C.beurttijdbenaming.Length; i++)
            {
                AIbeurtstat.Items.Add(C.beurttijdbenaming[i]);
            }
            AIbeurtstat.SelectedIndex = beurttijdstatus;
            AIbeurtstat.SelectedIndexChanged += AIbeurtstat_SelectedIndexChanged;
            // 
            // OptiesForm
            // 
            Controls.Add(naam1);
            Controls.Add(naam2);
            Controls.Add(spel1);
            Controls.Add(spel2);
            Controls.Add(AI1);
            Controls.Add(AI2);
            Controls.Add(Apply);
            Controls.Add(Cancel);
            Controls.Add(AIstat);
            Controls.Add(Kleurspel2);
            Controls.Add(Kleurspel1);
            Controls.Add(AIbeurtstat);
            //zet het icoontje links bovenin de form
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptiesForm));
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            //
            ClientSize = new Size(300, 300);
            MaximumSize = new Size(318, 347);
            MinimumSize = new Size(318, 347);
            Text = "Opties";
            StartPosition = FormStartPosition.CenterScreen;
        }
        //
        //sluit de form zonder de waarden te geven aan de optiesconfig
        //
        public void cancel(object sender,EventArgs ea)
        {
            Config = Config1;
           Close();
        }
        //
        //zet de Config string array in de optiesconfig file
        //en sluit dan de form
        public void apply(object sender,EventArgs ea)
        {          
            Close();
        }
        //
        //deze form word opgeroepen door de opties methode in de reversi form
        //
        public OptiesForm()
        {
            InitializeComponent();
        }
    }
}
