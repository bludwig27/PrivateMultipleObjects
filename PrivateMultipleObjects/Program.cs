using System;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Transactions;

namespace PrivateMultipleObjects
{
    class Planet
    {
        private string _Name;
        private double _Mass;
        private int _Temp;

        public Planet()
        {
            _Name = string.Empty;
            _Mass = 0;
            _Temp = 0;
        }
        public Planet(string name, double mass, int temp)
        {
            _Name = name;
            _Mass = mass;
            _Temp = temp;
        }
        public string getName() { return _Name; }
        public double getMass() { return _Mass; }
        public int getTemp() { return _Temp; }
        public void setName(string name) { _Name = name; }
        public void setMass(double mass) { _Mass = mass; }
        public void setTemp(int temp) { _Temp = temp; }

        public virtual void addChange()
        {
            Console.Write("Name: ");
            setName(Console.ReadLine());
            Console.Write("Mass (10^24 kg): ");
            setMass(Convert.ToDouble(Console.ReadLine()));
            Console.Write("Average surface temperature (C):");
            setTemp(int.Parse(Console.ReadLine()));
        }
        public virtual void print()
        {
            Console.WriteLine();
            Console.WriteLine($"  Name: {getName()}");
            Console.WriteLine($"  Mass: {getMass()} * 10^24 kg");
            Console.WriteLine($"  Temperature: {getTemp()} degrees Celcius");
        }
    }
    class Gas : Planet
    {
        private int _Moons;
        private bool _Rings;

        public Gas()
            : base()
        {
            _Moons = 0;
            _Rings = false;
        }
        public Gas(string name, double mass, int temp, int moons, bool rings)
            : base(name, mass, temp)
        {
            _Moons = moons;
            _Rings = rings;
        }
        public void setMoons(int moons) { _Moons = moons; }
        public void setRings(bool rings) { _Rings = rings; }
        public int getMoons() { return _Moons; }
        public bool getRings() { return _Rings; }
        public override void addChange()
        {
            string ringbool;
            base.addChange();
            Console.Write("Number of moons: ");
            setMoons(int.Parse(Console.ReadLine()));
            Console.Write("Rings? (y/n): ");
            ringbool = Console.ReadLine();
            if (ringbool == "y") { setRings(true); }
            else { setRings(false); }
        }
        public override void print()
        {
            base.print();
            Console.WriteLine($"  Moons: {getMoons()}");
            Console.WriteLine($"  Rings: {getRings()}");
            Console.WriteLine();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Planetary Database v0.0");
            Console.WriteLine();
            Console.WriteLine("How many planetary objects do you want to enter?");
            int maxPlanets;
            while (!int.TryParse(Console.ReadLine(), out maxPlanets))
                Console.WriteLine("Please enter a whole number");

            Planet[] planets = new Planet[maxPlanets];
            Console.WriteLine("How many gas giants do you want to enter?");
            int maxGas;
            while (!int.TryParse(Console.ReadLine(), out maxGas))
                Console.WriteLine("Please enter a whole number");

            Gas[] gas = new Gas[maxGas];

            int choice, rec, type;
            int planetsCounter = 0, gasCounter = 0;
            choice = Menu();
            while (choice != 4)
            {
                Console.WriteLine("Enter 1 for a gas giant or 2 for any other planetary object: ");
                while (!int.TryParse(Console.ReadLine(), out type))
                    Console.WriteLine("1 for gas giant or 2 for anything else: ");

                    switch (choice)
                    {
                        case 1:
                            if (type == 1)
                            {
                                if (gasCounter < maxGas)
                                {
                                    gas[gasCounter] = new Gas(); // places an object in the array instead of null
                                    gas[gasCounter].addChange();
                                    gasCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of gas giants has been added.");

                            }
                            else
                            {
                                if (planetsCounter < maxPlanets)
                                {
                                    planets[planetsCounter] = new Planet(); // places an object in the array instead of null
                                    planets[planetsCounter].addChange();
                                    planetsCounter++;
                                }
                                else
                                    Console.WriteLine("The maximum number of planets has been added.");
                            }

                            break;
                        case 2:
                            Console.Write("Enter the record number you want to change: ");
                            while (!int.TryParse(Console.ReadLine(), out rec))
                                Console.Write("Enter the record number you want to change: ");
                            rec--;  // subtract 1 because array index begins at 0
                            if (type == 1)
                            {
                                while (rec > gasCounter - 1 || rec < 0)
                                {
                                    Console.Write("The number you entered was out of range, try again");
                                    while (!int.TryParse(Console.ReadLine(), out rec))
                                        Console.Write("Enter the record number you want to change: ");
                                    rec--;
                                }
                                gas[rec].addChange();
                            }
                            else
                            {
                                while (rec > planetsCounter - 1 || rec < 0)
                                {
                                    Console.Write("The number you entered was out of range, try again");
                                    while (!int.TryParse(Console.ReadLine(), out rec))
                                        Console.Write("Enter the record number you want to change: ");
                                    rec--;
                                }
                                planets[rec].addChange();
                            }
                            break;
                        case 3:
                            if (type == 1)
                            {
                                for (int i = 0; i < gasCounter; i++)
                                    gas[i].print();
                            }
                            else
                            {
                                for (int i = 0; i < planetsCounter; i++)
                                    planets[i].print();
                            }
                            break;
                        default:
                            Console.WriteLine("You made an invalid selection. Please try again.");
                            break;
                    }
                choice = Menu();

            }
        }


        private static int Menu()
        {
            Console.WriteLine("Please make a selection:");
            Console.WriteLine("1-Add planet  2-Change planet  3-Display contents  4-Quit");
            int selection = 0;
            while (selection < 1 || selection > 4)
                while (!int.TryParse(Console.ReadLine(), out selection))
                    Console.WriteLine("1-Add  2-Change  3-Display  4-Quit");
            return selection;
        }
    }
}
