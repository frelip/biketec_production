using System;
using System.Collections.Generic;
using System.Text;

namespace Tool
{
    public struct Bestellung
    {
        int bestellmenge;
        int bestellkosten;

        //wenn > 4, dann kommts erst nächste Periode!
        //int lieferung;

        public int Bestellmenge
        {
            get { return bestellmenge; }
            set { bestellmenge = value; }
        }

        public int Bestellkosten
        {
            get { return bestellkosten; }
            set { bestellkosten = value; }
        }
    }

    public class Kaufteil : Teil
    {
        private double bestellkosten;
        private int erwartete_bestellung;
        private double preis;
        private double lieferdauer;
        private double abweichung_lieferdauer;
        private int diskontmenge;

        private Bestellung minBestellung;
        private Bestellung normalBestellung;
        private Bestellung maxBestellung;

        private List<ETeil> istTeil = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bestellPeriode">Periode, in der die Bestellung ausgeführt wurde</param>
        /// <param name="bestellmodus">Bestellmodus (4 = schnell | 5 = normal)</param>
        /// <param name="menge">Bestellmenge</param>
        public void addBestellung(int aktuellePeriode, int bestellPeriode, int bestellmodus, int menge)
        {
            //TODO
            // aktuellePeriode = 6
            //int aktuellePeriodTag = (aktuellePeriode - 1) * 5 + 1;
            int maxPeriodeTag = aktuellePeriode * 5;

            double tmp = 0.0;
            tmp = Math.Ceiling((this.Lieferdauer - this.Abweichung_lieferdauer) * 5 + 1);
            //tmp = Math.Floor((this.Lieferdauer - this.Abweichung_lieferdauer) * 5 + 1);
            double minDauer = (bestellPeriode - 1) * 5 + tmp;

            tmp = Math.Ceiling(this.Lieferdauer * 5 + 1);
            //tmp = Math.Floor(this.Lieferdauer * 5 + 1);
            double normalDauer = (bestellPeriode - 1) * 5 + tmp;

            tmp = Math.Ceiling((this.Lieferdauer + this.Abweichung_lieferdauer) * 5 + 1);
            //tmp = Math.Floor((this.Lieferdauer + this.Abweichung_lieferdauer) * 5 + 1);
            double maxDauer = (bestellPeriode - 1) * 5 + tmp;

            // 4 = Eilbestellung
            if (bestellmodus == 4 && (Math.Floor((this.Lieferdauer * 5 / 2) + 1)) < maxPeriodeTag)
            {
                normalBestellung.Bestellmenge = menge;

                minBestellung.Bestellmenge = menge;
                maxBestellung.Bestellmenge = menge;
            }
            else
            {
                if (minDauer < maxPeriodeTag)
                {
                    minBestellung.Bestellmenge = menge;
                }

                if (normalDauer < maxPeriodeTag)
                {
                    normalBestellung.Bestellmenge = menge;
                }

                if (maxDauer < maxPeriodeTag)
                {
                    maxBestellung.Bestellmenge = menge;
                }
            }
        }

        public Bestellung MinBestellung
        {
            get { return this.minBestellung; }
            set { this.minBestellung = value; }
        }

        public Bestellung NormalBestellung
        {
            get { return this.normalBestellung; }
            set { this.normalBestellung = value; }
        }

        public Bestellung MaxBestellung
        {
            get { return this.maxBestellung; }
            set { this.maxBestellung = value; }
        }

        public Kaufteil(int nummer, string bez)
            : base(nummer, bez)
        {

        }

        /// <summary>
        /// Gets or sets bereits besstellte aber noch nicht angekommene Menge
        /// </summary>
        /// <value>The erwartete bestellung.</value>
        public int ErwarteteBestellung
        {
            get
            {
                return erwartete_bestellung;
            }
            set
            {
                erwartete_bestellung = value;
            }
        }

        /// <summary>
        /// Gets or sets Einkaufspreis
        /// </summary>
        /// <value>The preis.</value>
        public double Preis
        {
            get
            {
                return this.preis;
            }
            set
            {
                this.preis = value;
            }
        }




        public double Lieferdauer
        {
            get
            {
                return this.lieferdauer;
            }
            set
            {
                this.lieferdauer = value;
            }
        }


        public double Abweichung_lieferdauer
        {
            get
            {
                return abweichung_lieferdauer;
            }
            set
            {
                abweichung_lieferdauer = value;
            }
        }



        public int Diskontmenge
        {
            get
            {
                return diskontmenge;
            }
            set
            {
                diskontmenge = value;
            }
        }


        public double Bestellkosten
        {
            get
            {
                return bestellkosten;
            }
            set
            {
                bestellkosten = value;
            }
        }

        /// <summary>
        /// wo wird das Teil verwendet.
        /// </summary>
        /// <value>Ist</value>
        public List<ETeil> IstTeilVon
        {
            get
            {
                if (this.istTeil == null)
                {
                    List<ETeil> res = new List<ETeil>();
                    foreach (ETeil teil in DataContainer.Instance.ETeilList)
                    {
                        if (teil.Nummer == 17)
                        { }
                        if (teil.Zusammensetzung.ContainsKey(this))
                        {
                            res.Add(teil);
                        }
                    }
                    this.istTeil = res;
                }
                
                return this.istTeil;
            }
        }


        public bool Equals(Kaufteil k)
        {
            if (this.nr == k.Nummer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    }


