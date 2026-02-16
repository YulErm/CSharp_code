using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3.Model
{
    public class Plane
    {
        private int length;
        private List<Passenger>[,] seats;

        public Plane(int length)
        {
            this.length = length;
            seats = new List<Passenger>[7, length + 2];

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < length + 2; j++)
                {
                    seats[i, j] = new List<Passenger>();
                }
            }
        }

        public void PrintPlane()
        {
            for (int x = 0; x < 7; x++)
            {
                if (x != 3)
                {
                    PrintSeats(x);
                }
                else
                {
                    PrintCorridor();
                }
            }
        }

        public void PrintSeats(int seatId)
        {
            string rowStr = "  | ";
            for (int j = 1; j < length + 1; j++)
            {
                if (seats[seatId, j].Count == 0)
                {
                    rowStr += " O";
                }
                else
                {
                    rowStr += $" {seats[seatId, j].Count}";
                }
            }
            System.Console.WriteLine(rowStr); // change to Debug if needed
        }

        public void PrintCorridor()
        {
            string corridorStr = "  ";
            for (int j = 0; j < length + 2; j++)
            {
                if (seats[3, j].Count == 0)
                {
                    corridorStr += "  ";
                }
                else
                {
                    corridorStr += $" {seats[3, j].Count}";
                }
            }
            System.Console.WriteLine(corridorStr); // change to Debug if needed
        }

        public void AddPassengers(List<Passenger> passengerList)
        {
            foreach (Passenger passenger in passengerList)
            {
                passenger.AddToPlane(this);
                Position newPosition = new Position(3, 0);
                seats[newPosition.Y, newPosition.X].Add(passenger);
            }
        }

        public bool IsEmpty(int row, int col)
        {
            // if position doesn't exist
            if (col < 0 || col >= length + 2)
            {
                return true; 
            }

            // if there's list, then returns false, true otherwise
            return seats[col, row].Count == 0;
        }

        public void MoveRow(int row, char seat)
        {
            int col = seat - 'A';  // converts seat to int (for ex. if seat is 'B' than col will be 'B' - 'A' = 1)
            // if position is empty
            if (IsEmpty(row + 1, col))
            {
                for (int i = 0; i < 7; i++)  // go through each current row
                {
                    for (int x = 0; x < 7; x++)
                    {
                        seats[col, row + 1].AddRange(seats[x, row]); // adds passenger to new position and clears previous position 
                        seats[x, row].Clear();
                    }  
                } 
            }
        }

        public void ReturnRow(int row)
        {
            for (int i = 0; i < 7; i++)  // going through all columns
            {
                foreach (var passenger in seats[i, row + 1])
                {
                    Position seatPosition = passenger.GetSeatPosition();  // get passenger seat before moving
                    seats[seatPosition.Y, seatPosition.X].Add(passenger);  // put passengers back in seats
                }

                seats[i, row + 1].Clear();  // clear row [3, row + 1]
            }
        }

        public void MovePassengers()
        {
            // Начинаем с конца коридора
            for (int j = length + 1; j > 0; j--)
            {
                foreach (var passenger in seats[3, j])
                {
                    Position newPosition = passenger.Move(); // Получаем новую позицию пассажира

                    // Проверяем, не выходит ли пассажир за границы массива
                    if (newPosition.Y >= 0 && newPosition.Y < 7 && newPosition.X >= 0 && newPosition.X < length + 2)
                    {
                        // Удаляем пассажира из старой позиции
                        seats[3, j].Remove(passenger);

                        // Добавляем пассажира в новую позицию
                        seats[newPosition.Y, newPosition.X].Add(passenger);
                    }
                }
            }
        }

        public bool BoardingFinished()
        {
            // check that aisle is empty
            if (seats[3, 0].Count == 0)
            {
                // check all seats, if they're not empty
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 1; j < length + 1; j++)
                    {
                        if (seats[i, j].Count == 0)
                        {
                            // if there was empty seat
                            return false;
                        }
                    }
                }

                // all seats taken
                return true;
            }

            // if there were passengers in aisle
            return false;
        }
    }
}
