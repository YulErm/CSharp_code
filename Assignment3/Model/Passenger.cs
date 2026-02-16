namespace Assignment3.Model
{
    public class Passenger
    {
        private int row;
        private char seat;
        private int bags;

        private Plane plane;
        private Position currentPosition;

        public Passenger(int row, char seat, int bagNo)
        {
            // checks if the seat letter is valid
            if (seat < 'A' || seat > 'F')
            {
                throw new ArgumentException("Invalid letter");
            }

            this.row = row;
            this.seat = seat;
            this.bags = bagNo * 4;  // the passenger needs 4 steps to pack each piece of luggage
            this.plane = null;
            this.currentPosition = null;
        }

        public Position? GetPosition()
        {
            // if there's plane and passenger has his seat in plane, then returns position
            if (currentPosition != null && plane != null)
            {
                // change position (ex. '3D' will be [3,2] for passenger, but for plane it will be [2,3]) 
                int newX = currentPosition.Y;
                int newY = currentPosition.X;
                return new Position(newX, newY);  // return position from plane view
            }
            return null;
        }

        public Position GetSeatPosition()
        {
            if (plane == null)
            {
                return null;
            }
            // change seat and row so position will be from plane view
            return new Position(seat, row);
        }
        public void AddToPlane(Plane plane)
        {
            this.plane = plane;
            // sets position of passenger to one in plane; position sets from passenger view, so we change [0,3] to [3,0]
            currentPosition = new Position(3, 0);
        }

        public void ForcedToMove(int x, int y)
        {
            // set new position, but we need to change coordinates to set them from pass. view
            currentPosition = new Position(y, x);
        }

        public bool CanSit()
        {
            // if there're no plane, or passenger is not in it, or passenger isn't in it's row,
            // or the passenger isn't in aisle (Y!=3), then false
            if (currentPosition == null || plane == null 
                || currentPosition.X != row || currentPosition.Y != 3)
            {
                return false;
            }

            // if passenger has aisle seat C or D
            if (seat == 'D' || seat == 'C') { return true; }
            else
            {
                char checkSeat = seat;
                switch (checkSeat)
                {
                    case 'A':
                        // check if seat B and C are empty
                        if (plane.IsEmpty(row, checkSeat+1) && plane.IsEmpty(row, checkSeat + 2)) { return true; }
                        return false;
                    case 'B':
                        // check if seat C is empty
                        if (plane.IsEmpty(row, checkSeat + 1)) { return true; }
                        return false;
                    case 'E':
                        // check if seat D is empty
                        if (plane.IsEmpty(row, checkSeat - 1)) { return true; }
                        return false;
                    case 'F':
                        // check if seat D and E are empty
                        if (plane.IsEmpty(row, checkSeat - 1) && plane.IsEmpty(row, checkSeat - 2)) { return true; }
                        return false;
                    default: return false;
                }
            }
        }

        public Position Move()
        {
            // if was not added to the plane, throw an exception
            if (plane == null)
            {
                throw new Exception("Passenger was not added to the plane");
            }

            // 
            if (currentPosition.X != row && plane.IsEmpty(currentPosition.X+1, 3)) { return new Position(currentPosition.X + 1, 3); }
            return new Position(0, 0);
        }
    }
}
