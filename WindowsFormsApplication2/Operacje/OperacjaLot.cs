﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class OperacjaLot : IOperacja
    {
        private Samolot samolot;
        private int pamiec;  // 0 - pierwsze wykonanie
        public OperacjaLot(Samolot samolot) 
        {
            pamiec = 0;
            this.samolot = samolot;
        }
        public override bool wykonajTick()
        {
            if (pamiec == 0)
            {
                if (samolot.AktualnyStan == Stan.WPowietrzu) {
                    pamiec = 1;
                }
                //if (samolot.getAktualnePaliwo() == 0) return false;
                if (samolot.AktualnyStan == Stan.Hangar) // to jest ogolnie do zmiany bo z hangaru nie moze od rauz leciec
                {
                    samolot.AktualnyStan = Stan.WPowietrzu;
                    pamiec = 1;
                }
                return true;

            }

            if (pamiec % 10 == 0)
            {
                samolot.AktualnaIloscPaliwa = samolot.AktualnaIloscPaliwa - 1;
            }

            pamiec++;

            if (pamiec > 10) pamiec = 1;

            if (samolot.AktualnyStan == Stan.WPowietrzu)
            {
                if (samolot.AktualnaIloscPaliwa <= 0) // było == sprobuje >
                {
                    samolot.AktualnyStan = Stan.Zniszczony;
                    return false;
                }

                return true;
            }
            return false;

        }

        public override void zatrzymaj()
        {
            throw new NotImplementedException();
        }

        public override Samolot getSamolot()
        {
            return samolot;
        }
    }
}