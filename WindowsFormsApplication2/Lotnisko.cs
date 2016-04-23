﻿using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Lotnisko : Form
    {
        private MenedzerSamolotow menedzerSamolotow;
        private MenedzerOperacji menedzerOperacji;

        public Lotnisko()
        {
            InitializeComponent();
            menedzerOperacji = new MenedzerOperacji(this);
            menedzerSamolotow = new MenedzerSamolotow(this, menedzerOperacji);
            panelSamolotow.MouseWheel += new MouseEventHandler(menedzerSamolotow.mouseWheelEventHangar);
            panelSamolotyWPowietrzu.MouseWheel += new MouseEventHandler(menedzerSamolotow.mouseWheelEventPowietrze); // do zaprogramowania

            this.labelTekstInformacje.Parent = panelInformacji;
            this.labelTekstInformacje.AutoSize = false;
            this.labelTekstInformacje.Size = new System.Drawing.Size(labelTekstInformacje.Parent.Size.Width, this.labelTekstInformacje.Size.Height);

            this.labelWPowietrzu.Parent = panelSamolotyWPowietrzu;
            this.labelWPowietrzu.AutoSize = false;
            this.labelWPowietrzu.Size = new System.Drawing.Size(this.labelWPowietrzu.Size.Width, this.labelWPowietrzu.Parent.Size.Height);

            this.labelInformacje.Parent = panelInformacji;

            this.labelHangar.Parent = panelSamolotow;
            this.labelHangar.AutoSize = false;
            this.labelHangar.Size = new System.Drawing.Size(labelHangar.Parent.Size.Width, this.labelHangar.Size.Height);

            schowajWszystkiePrzyciskiPanelu();
        }
        public Panel getPanelSamolotowPowietrze() { return this.panelSamolotyWPowietrzu; }
        public Panel getPanelSamolotow() { return this.panelSamolotow;  }
        public Label getLabelInformacje() { return this.labelInformacje;  }
        
     
        public void uaktualnijPrzyciskiPanelu(Miniatura aktualnieZaznaczony)
        {

            // bedziemy potem wyłapywac też typ samolotu

            schowajWszystkiePrzyciskiPanelu();

            if (!(aktualnieZaznaczony is Samolot) )
                return;
            
            Samolot aktualnieZaznaczonySamolot = (Samolot)aktualnieZaznaczony;
            

            Stan stanZaznaczonegoSamolotu = aktualnieZaznaczonySamolot.getStan();
            

            if (stanZaznaczonegoSamolotu == Stan.Tankowanie)
            {
                tankowanieCancel.Enabled = true;
                tankowanieCancel.Visible = true;
            }
            else if(stanZaznaczonegoSamolotu == Stan.Hangar)
            {
                kontrola.Enabled = true;
                kontrola.Visible = true;
                naPasStartowy.Enabled = true;
                naPasStartowy.Visible = true;
                tankowanie.Enabled = true;
                tankowanie.Visible = true;

                if (aktualnieZaznaczonySamolot.jestZatankowany())
                    tankowanie.BackColor = System.Drawing.Color.YellowGreen;
                else
                    tankowanie.BackColor = System.Drawing.Color.OrangeRed;

                
            }
        }

        private void schowajWszystkiePrzyciskiPanelu()
        {
            // mozna potem to na tablice przerobic
            kontrola.Enabled = false;
            kontrola.Visible = false;
            naPasStartowy.Enabled = false;
            naPasStartowy.Visible = false;
            tankowanie.Enabled = false;
            tankowanie.Visible = false;
            tankowanieCancel.Enabled = false;
            tankowanieCancel.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menedzerSamolotow.dbgDodajSamolot(0);
        }
   
        private void button2_Click(object sender, EventArgs e)
        {
            menedzerSamolotow.dbgDodajSamolot(1);
        }

        private void tankowanie_Click(object sender, EventArgs e)
        {
            menedzerSamolotow.tankujZaznaczonySamolot();
        }

        private void tankowanieCancel_Click(object sender, EventArgs e)
        {
           if(menedzerSamolotow.getZaznaczony() is Samolot) menedzerOperacji.zatrzymajOperacjeNaSamolocie((Samolot)menedzerSamolotow.getZaznaczony());
        }
    }
}
