using System;

namespace NotificationModelLibrary{
    public partial class User : IComparable<User>,IEquatable<User>{
        public int CompareTo(User? other){
            if(other is null) return 1;
            return string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString(){
            return $"Id: {Id} | Name: {Name} | Email: {Email} | Phone: {PhoneNumber} | Active: {IsActive}";
        }

        public bool Equals(User? other){
            if(other is null) return false;
            return Id == other.Id;
        }
    }
}