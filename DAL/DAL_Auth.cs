using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Auth;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_Auth
    {
        public static async Task Aunteticacion (string Usuario, string Contraseña)
        {
            string key = "AIzaSyDKzEcmyoow7KHD3kWkOiE9JFWwULkGHUo";
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(key));
            await authProvider.CreateUserWithEmailAndPasswordAsync(Usuario, Contraseña);
        }
    }
}
