using System;

namespace Z1
{
    public enum Gender
    {
        Male, Female
    }

    public class Student : IEquatable<Student>
    {
        public string Name { get; set; }
        public string Jmbag { get; set; }
        public Gender Gender { get; set; }

        public Student(string name, string jmbag)
        {
            Name = name;
            Jmbag = jmbag;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Jmbag != null ? Jmbag.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Gender;

                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Student)obj);
        }
        public bool Equals(Student other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Name, other.Name) && 
                   string.Equals(Jmbag, other.Jmbag) && 
                   Gender == other.Gender;
        }

        public static bool operator ==(Student s1, Student s2) => s1.Equals(s2);
        public static bool operator !=(Student s1, Student s2) => !(s1 == s2);
    }
}