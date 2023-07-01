using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_PR2_1_z4
{
	class Kalkulator : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		private string?
			wynik = "0",
			operacja = null
			;
		private double?
			operandLewy = null,
			operandPrawy = null
			;

		public string Wynik {
			get => wynik;
			set {
				wynik = value;
				PropertyChanged?.Invoke(
					this,
					new PropertyChangedEventArgs("Wynik")
					);
			}
		}
		public string Działanie {
			get
			{
				if (operandLewy == null)
					return "";
				else if (operandPrawy == null)
					return $"{operandLewy} {operacja}";
				else
					return $"{operandLewy} {operacja} {operandPrawy} = ";
			}
		}

		internal void WprowadźCyfrę(string? cyfra)
		{
			if (wynik == "0")
				if (cyfra == "0")
					return;
				else
					Wynik = cyfra;
			else
				Wynik += cyfra;
		}

		internal void WprowadźPrzecinek()
		{
			if (wynik.Contains(','))
				return;
			else
				Wynik += ',';
		}

		internal void ZmieńZnak()
		{
			if (wynik == "0")
				return;
			else if (wynik[0] == '-')
				Wynik = wynik.Substring(1);
			else
				Wynik = '-' + wynik;
		}

		internal void KasujZnak()
		{
			if (wynik == "0")
				return;
			else if (
				wynik.Length == 1
				|| (wynik.Length == 2 && wynik[0] == '-')
				|| wynik == "-0,"
				)
				Wynik = "0";
			else
				Wynik = wynik.Substring(0, wynik.Length - 1);
		}

		internal void CzyśćWszystko()
		{
			CzyśćWynik();
			operacja = null;
			operandLewy = operandPrawy = null;
			PropertyChanged?.Invoke(
				this,
				new PropertyChangedEventArgs("Działanie")
				);
		}

		internal void CzyśćWynik()
		{
			Wynik = "0";
		}

		internal void WprowadźOperacja(string operacja)
		{
			if (this.operacja != null)
			{
				WykonajDziałanie();
				this.operacja = operacja;
			}
			else
			{
				operandLewy = Convert.ToDouble(wynik);
				this.operacja = operacja;
				PropertyChanged?.Invoke(
					this,
					new PropertyChangedEventArgs("Działanie")
					);
			}

			wynik = "0";
		}

		internal void WykonajDziałanie()
		{
			if (operandPrawy == null)
				if (wynik == "0")
					operandPrawy = operandLewy;
				else
					operandPrawy = Convert.ToDouble(wynik);
			PropertyChanged?.Invoke(
				this,
				new PropertyChangedEventArgs("Działanie")
				);
			switch (operacja)
			{
				case "+": Wynik = (operandLewy + operandPrawy).ToString();
					break;
				case "-": Wynik = (operandLewy - operandPrawy).ToString();
					break;
				case "*": Wynik = (operandLewy * operandPrawy).ToString();
					break;
				case "/":
                    if (operandPrawy == 0)
                    {
                        Wynik = "Nie można dzielić przez 0";
                    }
                    else
                    {
                        Wynik = (operandLewy / operandPrawy).ToString();

                    }
					break;
				case "x^2": Wynik = (operandLewy * operandLewy).ToString();
					break;
				case "^":
                    Wynik = Math.Pow((double)operandLewy, (double)operandPrawy).ToString();
					break;
				case "mod":
                    if (operandPrawy == 0)
					{
                        return;
                    }
                    else
                    {
                        Wynik = (operandLewy % operandPrawy).ToString();
                    }
					break;
				case "1/x":
                    if (operandLewy == 0)
                    {
                        Wynik = "Nie można dzielić przez zero";
                        return;
                    }
                    else
                    {
                        Wynik = (1.0 / operandLewy).ToString();
                    }
					break;
				case "n!":
                    double wynik = 1;
                    for (int i = 1; i <= (int)operandLewy; i++)
                    {
                        wynik *= i;
                    }
                    Wynik = wynik.ToString();
					break;
				case "log":
                    Wynik = Math.Log10((double)operandLewy).ToString();
					break;
				case "log(x)":
					Wynik = Math.Log((double)operandLewy, (double)(operandPrawy)).ToString();
					break;
				case "round up":
                    Wynik = Math.Ceiling((double)operandLewy).ToString();
					break;
				case "round down":
                    Wynik = Math.Floor((double)operandLewy).ToString();
					break;
            }

            operandLewy = Convert.ToDouble(wynik);
		}
	}
}
