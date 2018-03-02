namespace Pragueparking2
{
	/// <summary>
	/// This class can be read throughout application and defines general values and constanst
	/// </summary>
	public static class GlobalDef
	{
		public static string AppTitle = "Prague Parking 2.0";
		public enum VehicleType : int { Car = 4, Trike = 3, MC = 2, Bike = 1 };

		public static int MaxParkingslot = 100;
		public const int MaxSlotSize = 4;
		public const int CarSize = 4;
		public const int TrikeSize = 3;
		public const int MCSize = 2;
		public const int BikeSize = 1;
		public static int NotFoundOrInvalid = -1;
	}
}
