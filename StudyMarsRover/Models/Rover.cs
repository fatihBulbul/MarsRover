using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StudyMarsRover.Models
{
    public class Rover
    {
        public int CurrentPositionX { get; private set; }
        public int CurrentPositionY { get; private set; }
        public char CurrentCompassDirection { get; private set; }
        public string MooveCommand { get; private set; }

        private readonly Plateau _plateau;

        public Rover(Plateau plateau)
        {
            _plateau = plateau;
        }

        /// <summary>
        /// Set rover start position
        /// </summary>
        public void SetStartPosition()
        {
            Console.WriteLine("Set start position");

            var startPositionInput = Console.ReadLine();
            var match = Regex.Match(startPositionInput, Constants.START_POSITION_REGEX);

            while (!match.Success)
            {
                Console.WriteLine("Invalid start position,please try again, format must be '{any integer}{space}{any integer}{space}{N,W,E OR S}'");
                startPositionInput = Console.ReadLine();
                match = Regex.Match(startPositionInput, Constants.START_POSITION_REGEX);
            }

            var startPositionValues = startPositionInput.Split(' ');

            CurrentPositionX = int.Parse(startPositionValues[0]);
            CurrentPositionY = int.Parse(startPositionValues[1]);
            CurrentCompassDirection = Convert.ToChar(startPositionValues[2]);
        }

        /// <summary>
        /// Set rover target position command 
        /// </summary>
        public void SetMoveCommand()
        {
            Console.WriteLine("Set move command");

            var commandInput = Console.ReadLine();
            var match = Regex.Match(commandInput, Constants.MOVE_REGEX);

            while (!match.Success)
            {
                Console.WriteLine("Invalid commad, please try again, only 'L', 'M' or 'R' can be used in the command");
                commandInput = Console.ReadLine();
                match = Regex.Match(commandInput, Constants.START_POSITION_REGEX);
            }

            MooveCommand = commandInput;
        }

        /// <summary>
        /// Rotate rover
        /// </summary>
        /// <param name="rotate">rotate must be 'L' Or 'R' Left, Right</param>
        public void Rotate(char rotate)
        {
            var nextCompassDirection = new Dictionary<string, char>
            {
                { "N_L", 'W' },
                { "N_R", 'E' },
                { "E_L", 'N' },
                { "E_R", 'S' },
                { "S_L", 'E' },
                { "S_R", 'W' },
                { "W_L", 'S' },
                { "W_R", 'N' },
            };

            CurrentCompassDirection = nextCompassDirection[CurrentCompassDirection + "_" + rotate];
        }

        /// <summary>
        /// Moove rover, 1 point
        /// </summary>
        public void Move()
        {
            switch (CurrentCompassDirection)
            {
                case 'N':
                    CurrentPositionY++;
                    break;
                case 'S':
                    CurrentPositionY--;
                    break;
                case 'W':
                    CurrentPositionX--;
                    break;
                case 'E':
                    CurrentPositionX++;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Rover move to target from 'MoveCommand'
        /// </summary>
        public void MoveToAddress()
        {
            for (int i = 0; i < MooveCommand.Length; i++)
            {
                var baseCommad = MooveCommand[i];
                var isMoveCommand = baseCommad == 'M';

                if (isMoveCommand)
                {
                    Move();

                    if (!_plateau.HasPoint(CurrentPositionX, CurrentPositionY))
                    {
                        throw new Exception("You went out of the plateau");
                    }
                }
                else
                {
                    Rotate(baseCommad);
                }
            }
        }
    }
}
