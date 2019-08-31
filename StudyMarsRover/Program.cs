using StudyMarsRover.Models;
using System;
using System.Collections.Generic;

namespace StudyMarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            var plateau = new Plateau();
            plateau.SetPlateauSize();

            var rovers = new List<Rover>();

            var roverAddFinish = false;

            while (!roverAddFinish)
            {
                var rover = new Rover(plateau);
                rover.SetStartPosition();
                rover.SetMoveCommand();
                rovers.Add(rover);

                Console.WriteLine("Add new rover: Y/N");
                var key = Console.ReadKey();
                var validKey = key.Key == ConsoleKey.Y || key.Key == ConsoleKey.N;

                while (!validKey)
                {
                    Console.WriteLine("Add new rover: Y/N");
                    key = Console.ReadKey();
                    validKey = key.Key == ConsoleKey.Y || key.Key == ConsoleKey.N;
                }

                roverAddFinish = key.Key == ConsoleKey.N;
                Console.WriteLine();
            }

            rovers.ForEach(rover => rover.MoveToAddress());
            rovers.ForEach(rover => Console.WriteLine($"{rover.CurrentPositionX} {rover.CurrentPositionY} {rover.CurrentCompassDirection}"));

            Console.ReadKey();
        }
    }
}