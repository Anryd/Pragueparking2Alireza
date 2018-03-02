using System;

namespace Pragueparking2
{
	public class ColorTheme
	{
		public enum ColorStyle { BlackOrWhite, Colorfull }
		public ColorStyle CurrentTheme { get; set; }

		// Environment specification
		public ConsoleColor ScreenBackgroundColor;
		public ConsoleColor TitleBarBackgroundColor { get; set; }
		public ConsoleColor TitleBarTextColor { get; set; }

		// Parking list
		public ConsoleColor ListNumberTextColor { get; set; }
		public ConsoleColor ListNumberBackgroundColor { get; set; }
		public ConsoleColor ListBackgroundColor { get; set; }

		// StatusBar
		public ConsoleColor StatusbarBackgroundColor { get; set; }
		public ConsoleColor TextColorForCar { get; set; }
		public ConsoleColor TextColorForTrike { get; set; }
		public ConsoleColor TextColorForMC { get; set; }
		public ConsoleColor TextColorForBike { get; set; }

		// Button Specification
		public ConsoleColor ButtonTextForeColor { get; set; }
		public ConsoleColor ButtonAccessKeyForeColor { get; set; }
		public ConsoleColor ButtonBackColor { get; set; }

		// Command prompt
		public ConsoleColor CommandPromptTextColor { get; set; }
		public ConsoleColor CommandPromptBackgroundColor { get; set; }
		public ConsoleColor CommandPromptErrorColor { get; set; }
		public ConsoleColor CommandPromptContinueTextColor { get; set; }


		public ColorTheme(ColorStyle color = ColorStyle.Colorfull)
		{
			if (color == ColorStyle.Colorfull)
			{
				CurrentTheme = ColorStyle.Colorfull;

				// Environment specification
				ScreenBackgroundColor = ConsoleColor.Black;
				TitleBarBackgroundColor = ConsoleColor.DarkCyan;
				TitleBarTextColor = ConsoleColor.White;

				// Parking list
				ListNumberTextColor = ConsoleColor.Red;
				ListNumberBackgroundColor = ConsoleColor.Gray;
				ListBackgroundColor = ConsoleColor.White;

				// StatusBar
				StatusbarBackgroundColor = ConsoleColor.Gray;
				TextColorForCar = ConsoleColor.DarkBlue;
				TextColorForTrike = ConsoleColor.DarkMagenta;
				TextColorForMC = ConsoleColor.DarkGreen;
				TextColorForBike = ConsoleColor.DarkYellow;

				// Button Specification
				ButtonTextForeColor = ConsoleColor.Gray;
				ButtonAccessKeyForeColor = ConsoleColor.Red;
				ButtonBackColor = ConsoleColor.DarkCyan;

				// Command prompt
				CommandPromptTextColor  = ConsoleColor.White;
				CommandPromptBackgroundColor = ConsoleColor.Black;
				CommandPromptErrorColor = ConsoleColor.Red;
				CommandPromptContinueTextColor = ConsoleColor.Gray;
			}
			else
			{
				CurrentTheme = ColorStyle.BlackOrWhite;

				// Environment specification
				ScreenBackgroundColor = ConsoleColor.Black;
				TitleBarBackgroundColor = ConsoleColor.DarkGray;
				TitleBarTextColor = ConsoleColor.White;

				// Parking list
				ListNumberTextColor = ConsoleColor.Black;
				ListNumberBackgroundColor = ConsoleColor.Gray;
				ListBackgroundColor = ConsoleColor.Black;

				// StatusBar
				StatusbarBackgroundColor = ConsoleColor.DarkGray;
				TextColorForCar = ConsoleColor.White;
				TextColorForTrike = ConsoleColor.White;
				TextColorForMC = ConsoleColor.White;
				TextColorForBike = ConsoleColor.White;

				// Button Specification
				ButtonTextForeColor = ConsoleColor.Black;
				ButtonAccessKeyForeColor = ConsoleColor.DarkGray;
				ButtonBackColor = ConsoleColor.Gray;

				// Command prompt
				CommandPromptTextColor = ConsoleColor.White;
				CommandPromptBackgroundColor = ConsoleColor.Black;
				CommandPromptErrorColor = ConsoleColor.White;
				CommandPromptContinueTextColor = ConsoleColor.Gray;
			}
		}
	}
}
