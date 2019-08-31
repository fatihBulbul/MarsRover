using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace StudyMarsRover.Models
{
    public class Plateau
    {
        public int XLength { get; private set; }
        public int YLength { get; private set; }
        public void SetPlateauSize()
        {
            Console.WriteLine("Set plateau size");
            var plateauSizeInput = Console.ReadLine();
            var match = Regex.Match(plateauSizeInput, Constants.PLATEAU_SIZE_REGEX);

            while (!match.Success)
            {
                Console.WriteLine("Invalid format,please try again, format must be '{any integer}{space}{any integer}'");
                plateauSizeInput = Console.ReadLine();
                match = Regex.Match(plateauSizeInput, Constants.PLATEAU_SIZE_REGEX);
            }

            var plateauSize = plateauSizeInput.Split(' ').Select(x => int.Parse(x) + 1).ToArray();

            XLength = plateauSize[0];
            YLength = plateauSize[1];
        }

        public bool HasPoint(int x, int y)
        {
            return x < XLength && y < YLength;
        }
    }
}
