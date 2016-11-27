using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace Reversi_V2
{
    class reversi : Form
    {
        private Button helpbutton, retrybutton;
        private Panel bordpanel, uipanel, buttonpanel, scorepanel, groottepanel, beurtstatuspanel, kleurstatuspanel;
        private Label beurtstatuslabel, scorelabel, winscorelabel, groottelabel, breedtelabel, hoogtelabel, helpstatuslabel;
        private ToolStripDropDownItem menu_grootte, menu_Opties;
        //
        //
        //
        private string[] Config = new string[6];
        private Constanten C = new Constanten();
        private Berekening B = new Berekening();
        private AI AI = new AI();
        private spelervariabelen speler1 = new spelervariabelen();
        private spelervariabelen speler2 = new spelervariabelen();
        private statusvariabelen Bord = new statusvariabelen();
        //
        private int[,] bordstatus, bordstatushelp, bordstatusai, bordstatusaihelp,zero;


        public reversi()
        {
            BackColor = Color.FromArgb(190, 225, 225);
            MinimumSize = new Size(700, 850);
            WindowState = FormWindowState.Maximized;
            SizeChanged += groottecheck;
            Text = "Reversi";
            //
            //
            //
            newgame();
            //
            //zet de ico file als icon van de form
            //
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reversi));
            Icon = ((Icon)(resources.GetObject("$this.Icon")));
            //
            //menustrip van de form
            //
            System.ComponentModel.ComponentResourceManager resourcesopties = new System.ComponentModel.ComponentResourceManager(typeof(OptiesForm));
            MenuStrip menustrip = new MenuStrip();
            menu_Opties = new ToolStripDropDownButton("Opties");
            menu_Opties.DropDownItems.Add("Opties", ((Icon)(resourcesopties.GetObject("$this.Icon"))).ToBitmap(),opties);
            menu_Opties.DropDownItems.Add("Save", null, save);
            menu_Opties.DropDownItems.Add("Load", null, load);
            menustrip.Items.Add(menu_Opties);
            menu_Opties.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            //
            //
            //
            menu_grootte = new ToolStripDropDownButton("Veld grootte");
            for (int i = 0; i < C.grootte.Length; i++)
            {
                menu_grootte.DropDownItems.Add(C.grootte[i], null, grootte);
            }
            ((ToolStripMenuItem)menu_grootte.DropDownItems[0]).Checked = true;
            menustrip.Items.Add(menu_grootte);
            //
            //
            //
            ToolStripButton menu;
            menu = new ToolStripButton("About");
            menu.Click += about;
            menustrip.Items.Add(menu);      
            //
            //
            //
            uipanel = new Panel();
            uipanel.Size = new Size(601, 120);
            uipanel.Location = new Point(ClientSize.Width / 2 - uipanel.Size.Width / 2, 25);
            uipanel.BorderStyle = BorderStyle.Fixed3D;
            uipanel.Anchor = AnchorStyles.None;
            //
            //
            //
            buttonpanel = new Panel();
            buttonpanel.Size = new Size(130, 65);
            buttonpanel.Location = new Point(10, 5);
            buttonpanel.BorderStyle = BorderStyle.Fixed3D;

            helpbutton = new Button();
            helpbutton.Size = new Size(50, 20);
            helpbutton.Text = "Help";
            helpbutton.Location = new Point(5, 5);
            helpbutton.Click += help;

            helpstatuslabel = new Label();
            helpstatuslabel.Size = new Size(30, 25);
            helpstatuslabel.Location = new Point(65, 7);
            helpstatuslabel.Text = ": uit";
            helpstatuslabel.Font = new Font(helpstatuslabel.Font, FontStyle.Bold);

            retrybutton = new Button();
            retrybutton.Size = new Size(50, 20);
            retrybutton.Text = "Retry";
            retrybutton.Location = new Point(5, 35);
            retrybutton.Click += retry;
            //
            //
            //
            kleurstatuspanel = new Panel();
            kleurstatuspanel.Size = new Size(580, 44);
            kleurstatuspanel.Location = new Point(10, 70);
            kleurstatuspanel.BorderStyle = BorderStyle.Fixed3D;
            kleurstatuspanel.BackColor = speler1.Kleur;
            //
            //
            //
            beurtstatuspanel = new Panel();
            beurtstatuspanel.Size = new Size(150, 30);
            beurtstatuspanel.Location = new Point(225, 77);
            beurtstatuspanel.BorderStyle = BorderStyle.Fixed3D;

            beurtstatuslabel = new Label();
            beurtstatuslabel.Size = new Size(150, 30);
            beurtstatuslabel.Location = new Point(0, 0);
            beurtstatuslabel.Text = speler1.Naam + " aan zet";
            beurtstatuslabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            //
            //
            scorepanel = new Panel();
            scorepanel.Size = new Size(330, 65);
            scorepanel.Location = new Point(150, 5);
            scorepanel.BorderStyle = BorderStyle.Fixed3D;

            winscorelabel = new Label();
            winscorelabel.Size = new Size(200, 40);
            winscorelabel.Location = new Point(125, 13);
            winscorelabel.Text = speler1.Naam + " heeft " + speler1.Winscore + " keer gewonnen" + "\n\n" + speler2.Naam + " heeft " + speler2.Winscore + " keer gewonnen";

            scorelabel = new Label();
            scorelabel.Size = new Size(150, 40);
            scorelabel.Location = new Point(10, 13);
            scorelabel.Text = speler1.Naam + ": " +speler1.Score+ "\n\n" + speler2.Naam + ": " + speler2.Score;
            //
            //
            //
            groottepanel = new Panel();
            groottepanel.Size = new Size(100, 65);
            groottepanel.Location = new Point(490, 5);
            groottepanel.BorderStyle = BorderStyle.Fixed3D;

            groottelabel = new Label();
            groottelabel.Size = new Size(90, 25);
            groottelabel.Location = new Point(10, 13);
            groottelabel.Text = "Veld Grootte:";

            breedtelabel = new Label();
            breedtelabel.Size = new Size(20, 25);
            breedtelabel.Location = new Point(20, 38);
            breedtelabel.Text = Convert.ToString(Bord.Breedte);

            hoogtelabel = new Label();
            hoogtelabel.Size = new Size(50, 25);
            hoogtelabel.Location = new Point(35, 38);
            hoogtelabel.Text = " x  " + Bord.Hoogte;
            //
            //
            //
            bordpanel = new Panel();
            bordpanel.Size = new Size(Bord.Breedte * 100 + 1, Bord.Hoogte * 100 + 1);
            bordpanel.Location = new Point(ClientSize.Width / 2 - bordpanel.Size.Width / 2,ClientSize.Height / 2 - bordpanel.Size.Height / 2+70);
            bordpanel.Paint += bordpaint;
            bordpanel.MouseClick += bordklik;
            bordpanel.BackColor = Color.White;
            bordpanel.Anchor = AnchorStyles.None;
            //om flickeren te voorkomen
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, bordpanel, new object[] { true });
            //
            //
            //
            groottepanel.Controls.Add(groottelabel);
            groottepanel.Controls.Add(breedtelabel);
            groottepanel.Controls.Add(hoogtelabel);

            scorepanel.Controls.Add(winscorelabel);
            scorepanel.Controls.Add(scorelabel);

            beurtstatuspanel.Controls.Add(beurtstatuslabel);

            buttonpanel.Controls.Add(helpstatuslabel);
            buttonpanel.Controls.Add(retrybutton);
            buttonpanel.Controls.Add(helpbutton);

            uipanel.Controls.Add(buttonpanel);
            uipanel.Controls.Add(scorepanel);
            uipanel.Controls.Add(groottepanel);
            uipanel.Controls.Add(beurtstatuspanel);
            uipanel.Controls.Add(kleurstatuspanel);

            Controls.Add(menustrip);
            Controls.Add(bordpanel);
            Controls.Add(uipanel);

            //
            //
            //
            retry(null, null);
        }
        //
        //een 
        //
        private void about(object o, EventArgs ea)
        {
            MessageBox.Show("Reversi V2.0(c) Coen Kenter October 2016"
                           , "Over \"Reversi V2\""
                           , MessageBoxButtons.OK
                           , MessageBoxIcon.Information
                           );
        }
        //
        //roept de optieform op en haalt informatie uit de config file die de optiemenu geeft
        //
        public void opties(object sender,EventArgs ea)
        {
            aiturnoff();
            OptiesForm optiemenu = new OptiesForm();
            optiemenu.config = Config;
            optiemenu.ShowDialog();

            Config = optiemenu.config;

            speler1.Naam = Config[4];
            speler2.Naam = Config[5];
            if (Config[4] == "")
            {
                speler1.Naam = C.benaming[int.Parse(Config[0])];
            }
            if (Config[5] == "" )
            {
                speler2.Naam = C.benaming[int.Parse(Config[1])];
            }           
            speler1.Kleur = C.kleuren[int.Parse(Config[0])];
            speler2.Kleur = C.kleuren[int.Parse(Config[1])];
            Bord.Beurttijd = C.beurttijdint[int.Parse(Config[3])];
            Bord.Aistat = int.Parse(Config[2]);
            aiturnon(Bord.Aistat, Bord.Beurttijd);
            

            
            bordpanel.Invalidate();
        }
        //
        //hier kan je een txt file maken die veel van de informatie bevat van het spel op dat moment
        //
        public void save(object sender,EventArgs ea)
        {
           
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string[] savefile = new string[12+ Bord.Hoogte];
            savefile[0] = speler1.Winscore.ToString();
            savefile[1] = speler2.Winscore.ToString();
            savefile[2] = speler1.Naam;
            savefile[3] = speler2.Naam;
            savefile[6] = Config[0];
            savefile[7] = Config[1];
            savefile[8] = Bord.Beurt.ToString();
            savefile[9] = Bord.Breedte.ToString();
            savefile[10] = Bord.Hoogte.ToString();
            savefile[11] = Bord.Aistat.ToString();
            for (int i = 0; i < Bord.Breedte; i++)
            {
                for (int j = 0; j < Bord.Hoogte; j++)
                {
                    savefile[j + 12] += bordstatus[i, j].ToString();
                }
            }
            savefile[4] = "";
            for (int i = 7; i < speler1.Kleur.ToString().Length-1; i++)
            {
                savefile[4] += speler1.Kleur.ToString()[i];
            }
            savefile[5] = "";
            for (int i = 7; i < speler2.Kleur.ToString().Length-1; i++)
            {
                savefile[5] += speler2.Kleur.ToString()[i];
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {          
                    File.WriteAllLines(saveFileDialog1.FileName, savefile);                                  
            }
        }
        //
        //hier laad je een txt file op die je een eerdere keer hebt gemaakt
        //
        public void load(object sender,EventArgs ea)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
           
            openFileDialog1.InitialDirectory = "\\Save";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    aiturnoff();
                    string[] loadfile = File.ReadAllLines(openFileDialog1.FileName);
                    speler1.Winscore = int.Parse(loadfile[0]);
                    speler2.Winscore = int.Parse(loadfile[1]);
                    speler1.Naam = loadfile[2];
                    speler2.Naam = loadfile[3];
                    speler1.Kleur = Color.FromName(loadfile[4]);
                    speler2.Kleur = Color.FromName(loadfile[5]);
                    Bord.Beurt = int.Parse(loadfile[8]);
                    Bord.Breedte = int.Parse(loadfile[9]);
                    Bord.Hoogte = int.Parse(loadfile[10]);
                    
                    Bord.Aistat = int.Parse(loadfile[11]);
                    Config[0] = loadfile[6];
                    Config[1] = loadfile[7];
                    Config[2] = loadfile[11];
                    Config[3] = "0";
                    Config[4] = loadfile[2];
                    Config[5] = loadfile[3];
                    
                    bordpanel.Size = new Size(600 * (Bord.Breedte / Bord.Hoogte) + 1, 601);
                    bordpanel.Location = new Point(ClientSize.Width / 2 - bordpanel.Size.Width / 2, ClientSize.Height / 2 - bordpanel.Size.Height / 2 + 70);
                    bordstatus = new int[Bord.Breedte, Bord.Hoogte];
                    for (int i = 0; i < Bord.Hoogte; i++)
                    {
                        string bord = loadfile[i + 12];
                        for (int j = 0; j < Bord.Breedte; j++)
                        {
                            string x = bord[j].ToString();
                            bordstatus[j, i] = int.Parse(x);
                        }
                    }
                    aiturnon(Bord.Aistat,Bord.Beurttijd);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            bordpanel.Invalidate();
        }
        //
        //zet alle waardes goed voor als je het spel opstart
        //
        public void newgame()
        {      
            speler1.Naam = C.benaming[0];
            speler2.Naam = C.benaming[5];
            speler1.Kleur = C.kleuren[0];
            speler2.Kleur = C.kleuren[5];
            speler1.Winscore = 0;
            speler2.Winscore = 0;
            Bord.Breedte = 6;
            Bord.Hoogte = 6;
            Bord.Win = 0;
            Bord.Beurt = 1;
            Bord.Beurttijd = 10;
            Bord.Aistat = 0;
            Bord.Help = 0;
            Config[0] = "0";
            Config[1] = "5";
            Config[2] = "0";
            Config[3] = "0";
            Config[4] = "";
            Config[5] = "";
        }
        //
        //zet de winscore van de spelers goed en zet het spel terug
        //
        public void winnaar()
        {
            if (Bord.Win == 1)
            {
                speler1.Winscore++;
                Bord.Win = 0;
            }
            else if (Bord.Win == 2)
            {
                speler2.Winscore++;
                Bord.Win = 0;
            }
            retry(null, null);
        }
        //
        //zet alles op de eerste waardes
        //
        public void retry(object sender, EventArgs ea)
        {
            int aista = aiturnoff();
            
            if (Bord.Win == 1)
            {
                speler1.Winscore++;
            }
            else if (Bord.Win == 2)
            {
                speler2.Winscore++;
            }
            bordstatus = new int[Bord.Breedte, Bord.Hoogte];
            bordstatushelp = new int[Bord.Breedte, Bord.Hoogte];
            bordstatusai = new int[Bord.Breedte, Bord.Hoogte];
            bordstatusaihelp = new int[Bord.Breedte, Bord.Hoogte];
            Bord.Beurt = 1;
            Bord.Win = 0;
            zero = new int[Bord.Breedte, Bord.Hoogte];
            for (int i = 0; i < Bord.Breedte; i++)
            {
                for (int j = 0; j < Bord.Hoogte; j++)
                {
                    zero[i, j] = 0;
                }
            }
            bordstatusai = zero;
            bordstatusaihelp = zero;
            bordstatushelp = zero;
            zero[Bord.Breedte / 2 - 1, Bord.Hoogte / 2 - 1] = 1;
            zero[Bord.Breedte / 2, Bord.Hoogte / 2] = 1;
            zero[Bord.Breedte / 2 - 1, Bord.Hoogte / 2] = 2;
            zero[Bord.Breedte / 2, Bord.Hoogte / 2 - 1] = 2;
            bordstatus = zero;
            bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);            
            bordpanel.Invalidate();
            Thread.Sleep(100);
            aiturnon(aista, Bord.Beurttijd);
        }
        //
        //wordt opgeroepen door de help knop en zet de helpstatus om
        //
        public void help(object sender, EventArgs ea)
        {
            if (Bord.Help == 0)
            {
                helpstatuslabel.Text = ": aan";
                Bord.Help = 1;
            }
            else
            {
                Bord.Help = 0;
                helpstatuslabel.Text = ": uit";
            }
            bordstatushelp = new int[Bord.Breedte, Bord.Hoogte];
            bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);
            bordpanel.Invalidate();
        }
        //
        //zoals de titel al zegt, het zet de tekst goed ten opzichte van de beurtstatus
        //
        public void updatetext(int beurt)
        {
            scorelabel.Text = speler1.Naam + ": " + speler1.Score + "\n\n" + speler2.Naam + ": " + speler2.Score;
            winscorelabel.Text = speler1.Naam + " heeft " + speler1.Winscore + " keer gewonnen" + "\n\n" + speler2.Naam + " heeft " + speler2.Winscore + " keer gewonnen";
           
            
            if (Bord.Win != 0)
            {
                if (Bord.Win == 1)
                {
                    beurtstatuslabel.Text = speler1.Naam + " heeft gewonnen";

                    kleurstatuspanel.BackColor = speler1.Kleur;
                }
                else if (Bord.Win == 2)
                {
                    beurtstatuslabel.Text = speler2.Naam + " heeft gewonnen";

                    kleurstatuspanel.BackColor = speler2.Kleur;
                }
                else if (Bord.Win == 3)
                {
                    beurtstatuslabel.Text = "Remise";

                    kleurstatuspanel.BackColor = Color.White;
                }
            }
            else
            {
                if (beurt == 1)
                {
                    beurtstatuslabel.Text = speler1.Naam + " aan zet";

                    kleurstatuspanel.BackColor = speler1.Kleur;
                }
                else if (beurt == 2)
                {
                    beurtstatuslabel.Text = speler2.Naam + " aan zet";

                    kleurstatuspanel.BackColor = speler2.Kleur;
                }
            }
            beurtstatuslabel.TextAlign = ContentAlignment.MiddleCenter;
            
        }
        //
        //neemt de muisklik en vraagt om een bord van de B.klikcalc
        //
        public void bordklik(object sender, MouseEventArgs mea)
        {
            
            int x = mea.X, y = mea.Y;
            x /= (600 / Bord.Hoogte);
            y /= (600 / Bord.Hoogte);
            bordstatushelp = new int[Bord.Breedte, Bord.Hoogte];
            bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);
            
            if (Bord.Win != 1 && B.helpcheck(Bord.Hoogte,Bord.Breedte,x,y,Bord.Beurt,bordstatus,C.check))
            {
                bordstatus = B.klikcalc(x, y, Bord.Breedte, Bord.Hoogte, Bord.Beurt, Bord.Aistat, bordstatus, C.check, bordstatushelp);
                bordpanel.Invalidate();
                if (Bord.Beurt == 1)
                {
                    Bord.Beurt = 2;
                }
                else
                {
                    Bord.Beurt = 1;
                }
            }
            else if (Bord.Win != 0)
            {
                winnaar();
            }
            else if (B.berekenaantal(Bord.Beurt + 2, B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check)) == 0)
            {
                if (Bord.Beurt == 1)
                {
                    Bord.Beurt = 2;
                }
                else
                {
                    Bord.Beurt = 1;
                }
                updatetext(Bord.Beurt);
                bordpanel.Invalidate();
                     
            }
        }
        //
        //zet het bord op de panel
        //
        public void bordpaint(object sender,PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            gr.DrawImage(B.setbitmap(speler1.Kleur,speler2.Kleur,Bord.Beurt,Bord.Breedte,Bord.Hoogte,bordstatus,Bord.Help,Bord.Aistat,C.check), 0, 0);
            speler2.Score = 0; speler1.Score = 0;
            foreach (int i in bordstatus)
            {
                if (i == 1)
                {
                    speler1.Score++;
                }
                if (i == 2)
                {
                    speler2.Score++;
                }
            }
            int aantal1 =B.berekenaantal(3, B.helpchecker(Bord.Hoogte, Bord.Breedte, 1, bordstatus, C.check));
            int aantal2 = B.berekenaantal(4, B.helpchecker(Bord.Hoogte, Bord.Breedte, 2, bordstatus, C.check));
            Bord.Win = B.winnaar(bordstatus, Bord.Hoogte, Bord.Breedte, C.check, aantal1,aantal2);
            updatetext(Bord.Beurt);
        }
        //
        //menu items 
        //
        //dit verandert de grootte van het veld
        public void grootte(object sender, EventArgs e)
        {

            int aista = aiturnoff();
            int checkedstatus = 0;
            for (int i = 0; i < C.grootte.Length; i++)
            {
                if (C.grootte[i] == Bord.Breedte + "x" + Bord.Hoogte)
                {
                    checkedstatus = i;
                }
            }
            for (int i = 0; i < C.grootte.Length; i++)
            {
                if (C.grootte[i] == sender.ToString())
                {
                    Size testsize = new Size(600 * (C.sizestatus[0, i] / C.sizestatus[1, i]) + 1, 601);
                    if ( testsize.Width <= Size.Width)
                    {
                        Bord.Breedte = C.sizestatus[0, i];
                        Bord.Hoogte = C.sizestatus[1, i];
                        checkedstatus = i;
                    }
                    
                }
            }
            for (int i = 0; i < C.grootte.Length; i++)
            {
                if (i == checkedstatus)
                {
                    ((ToolStripMenuItem)menu_grootte.DropDownItems[i]).Checked = true;
                }
                else
                {
                    ((ToolStripMenuItem)menu_grootte.DropDownItems[i]).Checked = false;
                }
            }
            breedtelabel.Text = Convert.ToString(Bord.Breedte);
            hoogtelabel.Text = " x  " + Bord.Hoogte;
            
            retry(null, null);
            bordpanel.Size = new Size(600 * (Bord.Breedte / Bord.Hoogte) + 1, 601);
            bordpanel.Location = new Point(ClientSize.Width / 2 - bordpanel.Size.Width / 2, ClientSize.Height / 2 - bordpanel.Size.Height / 2 + 70);
            aiturnon(aista, 0);      
        }
        //
        //zorgt er voor dat het bord binnen de form past en zo niet wordt het bord naar een passende verhouding gezet
        //
        public void groottecheck(object sender,EventArgs ea)
        {
            if (bordpanel.Size.Width > ClientSize.Width&& WindowState != FormWindowState.Minimized)
            {
                int checkedstatus = 0;
                for (int i = 0; i < C.grootte.Length; i++)
                {
                    if (C.grootte[i] == Bord.Breedte + "x" + Bord.Hoogte)
                    {
                        checkedstatus = i;
                    }
                }
                if (checkedstatus != 0)
                {
                    grootte(C.grootte[checkedstatus - 1], null);
                }                
            }
        }
        //
        //een ai die het eerste punt pakt dat de meest punten opleverd
        //
        public void aithreadsmart()
        {
            while (Bord.Aistat == 2)
            {
                if (Bord.Beurt == 2)
                {
                    bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);
                    if (B.berekenaantal(Bord.Beurt + 2, bordstatushelp) != 0)
                    {
                        Thread.Sleep(Bord.Beurttijd);

                        int[] xy = new int[2];

                        xy = AI.aismart(Bord.Aistat, Bord.Beurt, Bord.Breedte, Bord.Hoogte, bordstatus, C.check);
                        int x = xy[0], y = xy[1];

                        bordstatus = B.klikcalc(x, y, Bord.Breedte, Bord.Hoogte, Bord.Beurt, Bord.Aistat, bordstatus, C.check, bordstatushelp);

                        aibeurtswitch();
                    }
                    if (Bord.Win != 0 && Bord.Beurt == 2)
                    {
                        Bord.Beurt = 1;
                        bordpanel.Invalidate();
                    }
                   
                }
                
               
            }

        }
        //
        //een ai net zoals smartai die speelt tegen de random ai speelt
        //
        public void aithreadsmartvs()
        {
            while (Bord.Aistat == 3)
            {
                if (Bord.Beurt == 1)
                {
                    bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);
                }
                if (Bord.Beurt == 1 && B.berekenaantal(Bord.Beurt + 2, bordstatushelp) != 0)
                {
                    Thread.Sleep(Bord.Beurttijd);

                    int[] xy = new int[2];

                    xy = AI.aismart(Bord.Aistat, Bord.Beurt, Bord.Breedte, Bord.Hoogte, bordstatus, C.check);
                    int x = xy[0], y = xy[1];
                    if (x < Bord.Breedte && y < Bord.Hoogte)
                    {
                        bordstatus = B.klikcalc(x, y, Bord.Breedte, Bord.Hoogte, Bord.Beurt, Bord.Aistat, bordstatus, C.check, bordstatushelp);
                    }
                    aibeurtswitch();
                }
                else if (Bord.Win != 0 && Bord.Beurt == 1)
                {
                    Bord.Beurt = 2;
                    bordpanel.Invalidate();
                }
            }

        }
        //
        //een ai die hetzelfde werkt als de smart ai maar hij neemt een random coordinaat die de meeste punten opleverd
        //
        public void aithreadsmartrandom()
        {
            Random randomaantal = new Random();
            while (Bord.Aistat == 4 || Bord.Aistat == 3)
            {
                if (Bord.Beurt == 2)
                {
                    bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);
                    if (B.berekenaantal(Bord.Beurt + 2, bordstatushelp) != 0)
                    {
                        Thread.Sleep(Bord.Beurttijd);

                        int[] xy = new int[2];
                        xy = AI.aismartrandom(Bord.Beurt,Bord.Beurttijd,Bord.Aistat,Bord.Breedte,Bord.Hoogte,bordstatus,C.check,randomaantal);
                        int x = xy[0], y = xy[1];
                        if (x < Bord.Breedte && y < Bord.Hoogte)
                        {
                            bordstatus = B.klikcalc(x, y, Bord.Breedte, Bord.Hoogte, Bord.Beurt, Bord.Aistat, bordstatus, C.check, bordstatushelp);
                        }
                        aibeurtswitch();
                        
                    }

                    if (Bord.Win != 0 && Bord.Beurt == 2)
                    {
                        Bord.Beurt = 1;
                        bordpanel.Invalidate();
                    }
                }
                
            }

        }
        //
        //is een domme ai die het aller eerste punt pakt dat hij vind
        //       
        public void aithreaddumb()
        {

            while (Bord.Aistat == 1)
            {
                if (Bord.Beurt == 2)
                {
                    bordstatushelp = B.helpchecker(Bord.Hoogte, Bord.Breedte, Bord.Beurt, bordstatus, C.check);
                }               
                if (Bord.Beurt == 2 && B.berekenaantal(Bord.Beurt + 2,bordstatushelp) != 0)
                {
                    Thread.Sleep(Bord.Beurttijd);

                    int[] xy = new int[2];

                    xy = AI.aidumb(Bord.Aistat, Bord.Beurt, Bord.Breedte, Bord.Hoogte, bordstatus, C.check);

                    int x = xy[0], y = xy[1];
                    if (x < Bord.Breedte && y < Bord.Hoogte)
                    {
                        bordstatus = B.klikcalc(x, y, Bord.Breedte, Bord.Hoogte, Bord.Beurt, Bord.Aistat, bordstatus, C.check, bordstatushelp);
                    }
                    aibeurtswitch();
                }
                if (Bord.Win != 0 && Bord.Beurt == 2)
                {
                    Bord.Beurt = 1;
                    bordpanel.Invalidate();
                }
            }
        }
        //
        //switcht de beurt voor de ai
        //
        public void aibeurtswitch()
        {
           
            if (Bord.Beurt == 1)
            {
                Bord.Beurt = 2;
            }
            else if (Bord.Beurt == 2)
            {
                Bord.Beurt = 1;
            }
            bordpanel.Invalidate();
        }
        //
        //zet de ai uit
        //
        public int aiturnoff()
        {
            int ais = Bord.Aistat;
            Bord.Aistat = 0;
            return ais;
        }
        //
        //zet de ai aan
        //
        public void aiturnon(int aista, int beurttijd)
        {
            if (aista == 0)
            {
                Thread.Sleep(beurttijd);
            }            
            Bord.Aistat = aista;
            Thread aitest;
            if (Bord.Aistat == 1)
            {
                aitest = new Thread(aithreaddumb);
                aitest.IsBackground = true;
                aitest.Start();
            }
            if (Bord.Aistat == 2)
            {
                aitest = new Thread(aithreadsmart);
                aitest.IsBackground = true;
                aitest.Start();
            }
            if (Bord.Aistat == 3)
            {
                aitest = new Thread(aithreadsmartvs);
                aitest.IsBackground = true;
                aitest.Start();
                aitest = new Thread(aithreadsmartrandom);
                aitest.IsBackground = true;
                aitest.Start();
            }
            if (Bord.Aistat == 4)
            {
                aitest = new Thread(aithreadsmartrandom);
                aitest.IsBackground = true;
                aitest.Start();

            }
        }
    }

}
