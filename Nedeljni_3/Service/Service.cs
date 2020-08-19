using Nedeljni_3.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nedeljni_3.Service
{
    class Service
    {
        /// <summary>
        /// Gets all users from database
        /// </summary>
        /// <returns>list of users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public  bool IsRegisteredUser(string username)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    bool result = (from x in context.tblUsers where x.username == username select x).Any();
                    return result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="userName"></param>    
        /// <param name="pass"></param>
        /// <returns>user</returns>
        public tblUser AddUser(string userName, string pass)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    tblUser user = new tblUser();
                    user.fullname = userName;
                    user.username = userName;
                    user.password = pass;
                    user.role = "user";
                    context.tblUsers.Add(user);
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Adds the user
        /// </summary>
        /// <param name="userName"></param>    
        /// <param name="pass"></param>
        /// <returns>user</returns>
        public tblUser EditUser(tblUser user)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    tblUser userToEdit = (from x in context.tblUsers where x.userId == user.userId select x).First();
                    userToEdit.fullname = user.fullname;
                    userToEdit.username = user.username;                    
                    userToEdit.password = user.password;
                    userToEdit.role = "user";
                    userToEdit.userId = user.userId;
                    context.SaveChanges();
                    return user;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets user by forwarded username.
        /// </summary>
        /// <param name="userName">User's username</param>   
        /// <param name="pass">User's password</param>
        /// <returns>User.</returns>
        public tblUser GetUserByUsernameAndPass(string userName, string pass)
        {
            try
            {
                using (RecipeKeeperEntities context = new RecipeKeeperEntities())
                {
                    return context.tblUsers.Where(x => x.username == userName && x.password == pass).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
            
        }

        
    }
}
