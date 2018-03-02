using static Pragueparking2.GlobalDef;

namespace Pragueparking2
{
	class Program
	{
		static void Main(string[] args)
		{
			ParkingLot parkingLot = new ParkingLot();
			parkingLot.ParkingInitArrayRandom();

			ColorTheme theme = new ColorTheme(ColorTheme.ColorStyle.Colorfull);
			UserInterface ui = new UserInterface(theme, ref parkingLot);

			do
			{
				switch (ui.Menu())
				{
					case '1':	// Add Bike
						ui.Add(VehicleType.Bike);
						break;
					case '2':	// Add MC
						ui.Add(VehicleType.MC);
						break;
					case '3':	// Add Trike
						ui.Add(VehicleType.Trike);
						break;
					case '4':	// Add Car
						ui.Add(VehicleType.Car);
						break;
					case '5':	// Search
						ui.Search();
						break;
					case '6':	// Remove
						ui.Remove();
						break;
					case '7':	// Move
						ui.Move();
						break;
					case '8':	// Optimize
						ui.Optimize();
						break;
					case '9':	// Chage Color Theme
						ui.ColorChange();
						break;
					case '0':	// Quit
						return;
				}
			} while (true);
		}
	}
}
