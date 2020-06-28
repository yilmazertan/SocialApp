using System;
using Microsoft.AspNetCore.Identity;

namespace ServerApp.Model
{
    public class User:IdentityUser<int>
    {

       public string Name { get; set; }
       public string Gender { get; set; }
       public string City { get; set; }
       public string Country { get; set; }
       public DateTime DateOfBirth { get; set; }
       public string Intruduction { get; set; }
       public string Hobbies { get; set; }
       public DateTime Created { get; set; }
       public DateTime LastActive { get; set; }
        
    }
}