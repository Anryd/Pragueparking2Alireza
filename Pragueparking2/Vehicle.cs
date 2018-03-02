using System;

namespace Pragueparking2
{
	public class Vehicle
	{
		public virtual int Size { get; }
		public string RegNo;
		internal DateTime ArrivalTime;
	}

	public class Car : Vehicle
	{
		public Car(string regno, DateTime arrival)
		{
			RegNo = regno;
			ArrivalTime = arrival;
		}

		public override int Size { get => GlobalDef.CarSize; }
		internal String Color;
	}

	public class Tricke : Vehicle
	{
		public Tricke(string regno, DateTime arrival)
		{
			RegNo = regno;
			ArrivalTime = arrival;
		}
		public override int Size { get => GlobalDef.TrikeSize; }
		internal string Mark;

	}

	public class MC : Vehicle
	{
		public MC(string regno, DateTime arrival)
		{
			RegNo = regno;
			ArrivalTime = arrival;
		}
		public override int Size { get => GlobalDef.MCSize; }
		internal string Mark;
	}

	public class Bike : Vehicle
	{
		public Bike(string regno, DateTime arrival)
		{
			RegNo = regno;
			ArrivalTime = arrival;
		}

		public override int Size { get => GlobalDef.BikeSize; }
		internal string Identifier;

	}
}
