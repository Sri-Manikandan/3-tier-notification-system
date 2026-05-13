using System;
using System.Collections.Generic;

namespace NotificationModelLibrary{
    public partial class User{
        public int Id { get; set; }
        public string Name {get;set;}

        public string Email { get; set; }
        public string PhoneNumber {get ; set;}

        public bool IsActive {get;set;}
        public ICollection<Notification> Notifications { get; set; }

        public User(){
            
        }

        public User(string name, string email, string phoneNumber)
        {
            Name=name;
            Email = email;
            PhoneNumber=phoneNumber;
            IsActive = true;
        }

        public User(int id, string name, string email, string phoneNumber)
        {
            Id=id;
            Name=name;
            Email = email;
            PhoneNumber=phoneNumber;
            IsActive = true;
        }
    }
}
