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
			if (operacja == "+")
				Wynik = (operandLewy + operandPrawy).ToString();
			operandLewy = Convert.ToDouble(wynik);
		}
	}
}
