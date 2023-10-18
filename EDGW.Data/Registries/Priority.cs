namespace EDGW.Data.Registries
{
    public struct Priority : IEquatable<Priority> , IComparable<Priority>
    {
        internal Priority(int number)
        {
            Number = number;
        }
        public static Priority INNER_LOWEST { get; } = new(int.MinValue);
        public static Priority LOWEST { get; } = new(int.MinValue+100);
        public static Priority VERY_LOW { get; } = new(-100000);
        public static Priority LOW { get; } = new(-1000);
        public static Priority RELATIVELY_LOW { get; } = new(-10);
        public static Priority NORMAL { get; } = new(0);
        public static Priority INNER_HIGHEST { get; } = new(int.MaxValue);
        public static Priority HIGHEST { get; } = new(int.MaxValue-100);
        public static Priority VERY_HIGH { get; } = new(100000);
        public static Priority HIGH { get; } = new(1000);
        public static Priority RELATIVELY_HIGH { get; } = new(10);
        public int Number { get; }
        public static bool operator >(Priority a, Priority b)
        {
            return a.Number > b.Number;
        }
        public static bool operator <(Priority a, Priority b)
        {
            return a.Number < b.Number;
        }

        public static bool operator ==(Priority left, Priority right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Priority left, Priority right)
        {
            return !(left == right);
        }

        public override bool Equals(object? obj)
        {
            return obj is Priority priority && Equals(priority);
        }

        public bool Equals(Priority other)
        {
            return Number == other.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Number);
        }

        public int CompareTo(Priority other)
        {
            if (this == other) return 0;
            if (this > other) return 1;
            return -1;
        }
    }
}
