using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGildedRose.Models;

namespace TheGildedRose.Interfaces
{
    /// <summary>
    /// ----------------------------------------------------------------------------------------------------------
    /// "IUserAuthenication"        User Authenication Interface. Provides access a list of functions that help 
    ///                             with authenicating a user.
    /// ----------------------------------------------------------------------------------------------------------
    /// </summary>
    public interface IUserAuthenication
    {
        bool AuthenicateUser(User user);
    }
}
