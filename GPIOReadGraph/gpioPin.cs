using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPIOReadGraph
{
    public class  gpioPin
    {
		//	Названия колонок пинов, получается при получении из устройства
        public static string[] g_pins = { };// { "MODE", "PULL_SEL", "DIN", "DOUT", "PULL EN", "DIR", "IES" };

		//	Название пина
        public String pinID;
		//	Значение пина
        public String Value;

		//	Не обновлять состояние пина
        public bool rowDisable;
		//	Счетчик таймера гашения обновления
        private int nChanged;
		//	Предыдущее Значение пина
        private String oldValue;
		

        public gpioPin(string pinValue, string pID)
        {
            Value = pinValue;
            pinID = pID;

            nChanged = 0;
            rowDisable = false;
            oldValue = "";
        }
		//	Задать новое значение пина
        public bool setValue(string pinCFG)
        {
            if (pinCFG == Value) return false;

            oldValue = Value;
            nChanged = 20;
            Value = pinCFG;
            return true;
        }
		//	Статус изменения пина
        public bool isChanged()
        {
            return nChanged > 0;
        }
		//	Уменьшить счетчик задержки 
        public void tickChange()
        {
            if (!isChanged()) return;
            nChanged -= 1;
        }
		//	Получить статус изменения конкретного пина
        public bool IsChangedPin(int nPin)
        {
            if (!isChanged()) return false;
            if (nPin > Value.Length) return false;
            if (nPin > oldValue.Length) return true;

            return Value[nPin] != oldValue[nPin];
        }
		//	Получить имя пина по его номеру
        public string pinToString(int nPin)
        {
            if (nPin < 0 || Value.IndexOf('-') >= 0) return "";

            string val = Value[nPin].ToString();

            switch (g_pins[nPin])
            {
                case "MODE":
                    return string.Format("GPIO_MODE_{0}", val);
                case "PULL_SEL":
                    return int.Parse(val) > 0 ? "PULL SEL" : "";
                case "PULL EN":
                    return int.Parse(val) > 0 ? "PULL EN" : "";
                case "DIN":
                    return int.Parse(val) > 0 ? "IN" : "";
                case "DOUT":
                    return int.Parse(val) > 0 ? "OUT" : "";
                case "DIR":
                    return int.Parse(val) > 0 ? "DEF_OUT" : "DEF_IN";
				case "IES":
				case "SMT":
					return int.Parse(val) > 0 ? g_pins[nPin] : "";
			}
			return val;
        }
		//	Получить номер пина по его имения
        public int getPinIndex(string pinName)
        {
            return Array.IndexOf(g_pins, pinName);
        }
    };
}
