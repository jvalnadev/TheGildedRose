using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TheGildedRose.Interfaces;
using TheGildedRose.Models;

namespace TheGildedRose.Implementations
{
    public class UserAuthenicationImpl : IUserAuthenication 
    {

        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "IList<User>"       A list of users to simulate existing users on the database. The password doesn't need
        ///                     to be stored on the database. We're only storing the Hashed value of it. 
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        IList<User> users = new List<User>()
        {
            new User { Username="tonyawad",City="Ottawa",HashedPassword="8b4410a25b2029603ef2deded9dc75d4c3602ea2986e5c788635f9ddce2e0cb3",APIAccessToken = "f87b72c4d44b434af8e2bb1486000fe2b89865172d51263c080e167b002473df",TokenIssueDate = new DateTime(2016,2,1)},
            new User { Username="kennykolstad",City="California",HashedPassword="7973f659770047c02741b08fa2c3cb30bac6a3d17461e5aef0cc1b8154504d0c",APIAccessToken = "af3ee1ad42ef87c8d9939b63db895283c900ab9bb46037060014a0b457bcee07",TokenIssueDate = new DateTime(2016,2,29)},
            new User { Username="jedidjabourgeois",City="Ottawa",HashedPassword="0f7e21f35679b867dfa53c827800d9708d716c9d31ff80a4eed3fcd3a37dd2ff",APIAccessToken = "d52cf647176eb094940d8d60236e9627951e2a5731363404b5622f93748ee191",TokenIssueDate = new DateTime(2016,2,29)}            
        };


        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "AuthenicateUser"       this function authenicates a user and returns true or false to indicate if a user 
        ///                         is authenicated or not. It also verifies the token issue date to make sure the
        ///                         token is still valid. Token persistence is set 14 days. 
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="user"></param>
        public bool AuthenicateUser(User user)
        {
            //1. Verify if Username exists
            User searchUser = users.FirstOrDefault(u=>u.Username == user.Username);
            if (searchUser == null) return false;

            //2. Verify that token issue date hasn't been issued longer than 14 days.
            if ((DateTime.Now - searchUser.TokenIssueDate).TotalDays > 14) return false;
            
            //3. Regenerate API Access Token and compare with user provided token
            if(getAPIAccessToken(searchUser) == user.APIAccessToken) return true;

            //4. Return false if there is no match.
            return false;
        }


        #region PrivateMethods
        
        /// <summary>
        /// ----------------------------------------------------------------------------------------------------------
        /// "getHashedPassword_Sha256"  this function has been used to generate the hash password stored for users.
        ///                             It basically takes a user provided password adds some random keys, concat
        ///                             the user name decorated with random keys as well and finally it generates 
        ///                             an SHA256 hash and returns it as a string.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="user"></param>
        private string getHashedPassword_Sha256(User user)
        {
            //1. Append random keys around password and username
            string key = string.Concat("[@]" + user.Password + "[@@]" + "[@]" + user.Username + "[@@]");

            //2. Let the hash function do its magic.
            byte[] bytes = Encoding.Unicode.GetBytes(key);
            SHA256Managed hashstring_1 = new SHA256Managed();
            byte[] hash = hashstring_1.ComputeHash(bytes);
            string hashString_2 = string.Empty;
            foreach (byte x in hash)
            {
                hashString_2 += String.Format("{0:x2}", x);
            }
            //zz. Return hashed password.
            return hashString_2;
        }

        /// <summary>
        ///  ----------------------------------------------------------------------------------------------------------
        /// "getAPIAccessToken"     this function has been used to generate Access Token which gives authenication to 
        ///                         users to consume different API functions such as BuyItem. It basically takes a 
        ///                         user object and generate an API Access Token and returns it as a string.
        /// ----------------------------------------------------------------------------------------------------------
        /// </summary>
        /// <param name="user"></param>
        private string getAPIAccessToken(User user)
        {
            //1. Append random keys around Username, Password, City and TokenIssueDate to generate API Access Token.
            string key = string.Concat("[@]" + user.Username + "[@@]" + "[@]" + user.HashedPassword + "[@@]" + user.City + "[@]" + user.TokenIssueDate.ToString() + "[@@]");

            //2. Let the hash function generate a new hash value.
            byte[] bytes = Encoding.Unicode.GetBytes(key);
            SHA256Managed hashstring_1 = new SHA256Managed();
            byte[] hash = hashstring_1.ComputeHash(bytes);
            string hashString_2 = string.Empty;
            foreach (byte x in hash)
            {
                hashString_2 += String.Format("{0:x2}", x);
            }
            //zz. Return API Access Token
            return hashString_2;
        }

        #endregion

    }


}