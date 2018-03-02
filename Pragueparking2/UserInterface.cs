using System;
using System.Text.RegularExpressions;
using static Pragueparking2.GlobalDef;

namespace Pragueparking2
{
	public class UserInterface
	{
		public ParkingLot Parking;	// Reference to a parkingLot class

		public ColorTheme colortheme { get; set; }

		// Environment specification
		public const int ButtonRowTop = 27;
		public const int CommandPromptTop = 28;
		public const int StatusBarTop = 26;


		// Button Specification
		const int ButtonWidthSize = 11;
		const int ButtonHeightSize = 1;

		public UserInterface(ColorTheme cth, ref ParkingLot parking)
		{
			colortheme = cth;	// Set Color theme for UI
			Parking = parking;	// Set Parking reference
		}

		public void InitScreen()
		{
			Console.SetWindowSize(120, 30);
			Console.BufferHeight = 30;
			Console.BufferWidth = 120;

			Console.BackgroundColor = colortheme.ScreenBackgroundColor;
			Console.Clear();
			DrawBox(0, 1, 120, 25, colortheme.ListBackgroundColor);	// Draw background box for vehicles list
		}


		public char Menu()
		{
			InitScreen();

			// Add Header
			DrawHeader(GlobalDef.AppTitle);

			// Parking List
			ShowParking();

			// Show a status bar to show status of parking
			ShowParkingStatus();

			int ButtonPosition = 12;
			// Add Bike
			DrawButton(ButtonPosition * 0, ButtonRowTop, "1", "Add Bike");
			DrawButton(ButtonPosition * 1, ButtonRowTop, "2", "Add MC");
			DrawButton(ButtonPosition * 2, ButtonRowTop, "3", "Add Trike", 12);	// Because button title is longer
			DrawButton(ButtonPosition * 3 + 1, ButtonRowTop, "4", "Add Car");
			DrawButton(ButtonPosition * 4 + 1, ButtonRowTop, "5", "Search");
			DrawButton(ButtonPosition * 5 + 1, ButtonRowTop, "6", "Remove");
			DrawButton(ButtonPosition * 6 + 1, ButtonRowTop, "7", "Move");
			DrawButton(ButtonPosition * 7 + 1, ButtonRowTop, "8", "Optimize");

			// Change button title accordingly to current button function
			if (colortheme.CurrentTheme == ColorTheme.ColorStyle.Colorfull)
				DrawButton(ButtonPosition * 8 + 1, ButtonRowTop, "9", "No Color");
			else
				DrawButton(ButtonPosition * 8 + 1, ButtonRowTop, "9", "Color");

			DrawButton(ButtonPosition * 9 + 1, ButtonRowTop, "0", "Quit");

			return ShowCommandPrompt();
		}


		public void ShowParking()
		{
			int left = 0, top = 1;
			for (int i = 0; i < GlobalDef.MaxParkingslot; i++)
			{
				// Write parking table numbers
				DrawBox(left, top, 3, 1, colortheme.ListNumberBackgroundColor);	// Draw background column for numbers
				Console.ForegroundColor = colortheme.ListNumberTextColor;
				Console.SetCursorPosition(left, top);
				Console.Write(i + 1);

				// Set color and position to write list of vehicles to place
				Console.BackgroundColor = colortheme.ListBackgroundColor;
				Console.SetCursorPosition(left + 3, top);

				// Write each vehicle in slot in a row
				var lst = Parking.Content(i);
				foreach (var item in lst)
				{
					if (item is Car)
						Console.ForegroundColor = colortheme.TextColorForCar;
					else if (item is Tricke)
						Console.ForegroundColor = colortheme.TextColorForTrike;
					else if (item is MC)
						Console.ForegroundColor = colortheme.TextColorForMC;
					else if (item is Bike)
						Console.ForegroundColor = colortheme.TextColorForBike;
					else
						continue;

					Console.Write(item.RegNo + " ");
				}

				top++;	// Move to next line

				if ((top - 1) % 25 == 0)	// if reach to the end of column go up to next column
				{
					left += 30;	// Go to next column
					top = 1;	// Move to first line
				}
			}
		}



		public void ShowParkingStatus()
		{
			// StatusBar
			DrawBox(0, StatusBarTop, 120, 1, colortheme.StatusbarBackgroundColor);

			int NumCar = Parking.GetCount(VehicleType.Car);
			int NumMC = Parking.GetCount(VehicleType.MC);
			int NumTrike = Parking.GetCount(VehicleType.Trike);
			int NumBike = Parking.GetCount(VehicleType.Bike);

			Console.SetCursorPosition(0, StatusBarTop);
			Console.ForegroundColor = colortheme.TextColorForCar;
			Console.Write("Car (now: " + NumCar + ", free: " + Parking.CountFreeSpace(VehicleType.Car) + ")");

			Console.SetCursorPosition(30, StatusBarTop);
			Console.ForegroundColor = colortheme.TextColorForTrike;
			Console.Write("Trike (now: " + NumTrike + ", free: " + Parking.CountFreeSpace(VehicleType.Trike) + ")");

			Console.SetCursorPosition(60, StatusBarTop);
			Console.ForegroundColor = colortheme.TextColorForMC;
			Console.Write("MC (now: " + NumMC + ", free: " + Parking.CountFreeSpace(VehicleType.MC) + ")");

			Console.SetCursorPosition(90, StatusBarTop);
			Console.ForegroundColor = colortheme.TextColorForBike;
			Console.Write("Bike (now: " + NumBike + ", free: " + Parking.CountFreeSpace(VehicleType.Bike) + ")");
		}


		public void DrawBox(int left, int top, int x, int y, ConsoleColor color)
		{
			Console.SetCursorPosition(left, top);
			Console.BackgroundColor = color;
			Console.ForegroundColor = color;

			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					Console.SetCursorPosition(left + i, top + j);
					Console.Write(" ");
				}
			}
		}


		public void DrawButton(int Left, int Top, string AccessKey, string Title, int Width = ButtonWidthSize)
		{
			DrawBox(Left, Top, Width, ButtonHeightSize, colortheme.ButtonBackColor);
			Console.SetCursorPosition(Left, Top);
			Console.ForegroundColor = colortheme.ButtonAccessKeyForeColor;
			Console.Write(AccessKey);
			Console.ForegroundColor = colortheme.ButtonTextForeColor;
			Console.Write(" " + Title);
		}


		public void DrawHeader(string Title)
		{
			DrawBox(0, 0, 120, 1, colortheme.TitleBarBackgroundColor); // Title box
			Console.ForegroundColor = colortheme.TitleBarTextColor;
			Console.SetCursorPosition((Console.WindowWidth - Title.Length) / 2, 0);
			Console.Write(Title);

			Console.Title = Title;  // Windows Title

		}

		public char ShowCommandPrompt()
		{
			Console.SetCursorPosition(0, CommandPromptTop);
			Console.ForegroundColor = colortheme.CommandPromptTextColor;
			Console.BackgroundColor = colortheme.CommandPromptBackgroundColor;
			Console.Write("Please, select on option > ");

			return Console.ReadKey(true).KeyChar;
		}


		public void Add(VehicleType vehicle)
		{
			string RegNo = GetRegNo();

			if (RegNo == null)
			{
				return;
			}
			// Check for duplicate
			int Index = Parking.Search(RegNo);
			if (Index != NotFoundOrInvalid)
			{
				ShowError("Reg Number" + RegNo + " already exist in " + (Index + 1) + ". ");
				return;
			}

			int ParkingNumber = Parking.Add(RegNo, vehicle);

			if (ParkingNumber != NotFoundOrInvalid)
				PausePrompt("Vehicle " + RegNo + " will be added to slot number " + (ParkingNumber + 1));
		}


		public void Search()
		{
			string RegNo = GetRegNo();
			if (RegNo == null)
			{
				return;
			}
			int ParkingNumber = Parking.Search(RegNo);
			if (ParkingNumber != NotFoundOrInvalid)
				PausePrompt("Vehicle " + RegNo + " found in parking slot number " + (ParkingNumber + 1) + ". ");
			else
				ShowError(RegNo + " was not found.");
		}


		public void Remove()
		{
			string RegNo = GetRegNo("Enter vehicle registration number to remove > ");
			if (RegNo == null)	// If Reg No is not valid exit operation
			{
				return;
			}
			var vehicle = Parking.GetVehicle(RegNo);
			int ParkingNumber = Parking.Remove(RegNo);
			if (ParkingNumber != NotFoundOrInvalid)
			{
				PausePrompt(RegNo + " will be removed from parking slot number " + (ParkingNumber + 1)
					+ ", time in parking: " + (DateTime.Now - vehicle.ArrivalTime).ToString(@"hh\:mm\:ss"));
			}
		}


		public void Move()
		{
			string RegNo = GetRegNo("Enter vehicle registration number to move > ");
			if (RegNo == null)
			{
				return;
			}
			
			// Check if reg number aldready exist
			if (Parking.Search(RegNo) == NotFoundOrInvalid)
			{
				ShowError("Reg Number " + RegNo + " Not found!");
				return;
			}

			int NewIndex = GetSlotNumber();		// Get new position from user

			// Check new position for validation
			if (NewIndex == NotFoundOrInvalid)
			{
				return;
			}

			Vehicle v = Parking.GetVehicle(RegNo);					// Get vehicle object from registration number
			int OldIndex = Parking.Move(RegNo, NewIndex - 1);		// Minus -1 to fit in a zero index array

			if (OldIndex != NotFoundOrInvalid)
				PausePrompt("Vehicle " + RegNo + " will be moved from parking slot number " + (OldIndex + 1) + " to " + (NewIndex) + ". ");
			else
			{
				ShowError("Moving " + RegNo + " to " + (NewIndex) + " is impossible. ");
			}
		}

		public void Optimize()
		{
			int Counter = Parking.Optimize();

			if (Counter != 0)
			{
				ClearCommandPrompt();
				ShowError("Optimization moved " + Counter + " of vehicles.");
			}
			else
			{
				ClearCommandPrompt();
				ShowError("No optimization is possible.");
			}
		}


		public void ColorChange()
		{
			if (colortheme.CurrentTheme == ColorTheme.ColorStyle.Colorfull)
			{
				colortheme = new ColorTheme(ColorTheme.ColorStyle.BlackOrWhite);
			}
			else
			{
				colortheme = new ColorTheme(ColorTheme.ColorStyle.Colorfull);
			}
		}


		public int GetSlotNumber(string prompt = "Enter an empty slot between 1 to 100> ")
		{
			// Set text prompt
			ClearCommandPrompt();
			Console.Write(prompt);
			string sNewPlace = Console.ReadLine().Trim();

			int iNewPlace;
			if (Int32.TryParse(sNewPlace, out iNewPlace))
			{
				// Validate for range
				if (iNewPlace < 1 || iNewPlace > GlobalDef.MaxParkingslot)
				{
					ShowError("Value is out of range.");
					return NotFoundOrInvalid;
				}
			}
			return iNewPlace;
		}


		public void ClearCommandPrompt()
		{
			DrawBox(0, CommandPromptTop, 120, 1, colortheme.CommandPromptBackgroundColor);
			Console.SetCursorPosition(0, CommandPromptTop);
			Console.ForegroundColor = colortheme.CommandPromptTextColor;
		}


		public void ShowError(string ErrorMessage)
		{
			Console.ForegroundColor = colortheme.CommandPromptErrorColor;
			Console.Write(ErrorMessage);
			PausePrompt();
		}

		public void PausePrompt(string Message = "")
		{
			if (Message.Length > 0)
			{
				ClearCommandPrompt();
				Console.ForegroundColor = colortheme.CommandPromptTextColor;
				Console.SetCursorPosition(0, CommandPromptTop);
				Console.Write(Message);
			}

			Console.ForegroundColor = colortheme.CommandPromptContinueTextColor;
			Console.Write(" Press any key to continue ...");
			Console.ReadKey(true);
		}


		public String GetRegNo(string prompt = "Enter vehicle registration number > ")
		{
			// Set text prompt
			ClearCommandPrompt();
			Console.Write(prompt);

			bool IsValid = true;
			string strRegNo = String.Empty;

			strRegNo = Console.ReadLine().Trim();

			// Validate for Alphabet and numbers
			if (!Regex.IsMatch(strRegNo, @"^[a-zA-Z0-9]+$"))
			{
				ShowError("Registration number must be just letters and numbers.");
				IsValid = false;
			}

			// Validate for string length
			if ((strRegNo.Length < 2 || strRegNo.Length > 25) && IsValid == true)
			{
				ShowError(" Invalid registration number.");
				IsValid = false;
			}

			if (IsValid)
			{
				return strRegNo.ToUpper();
			}
			else
			{
				return null;
			}
		}
	}
}
