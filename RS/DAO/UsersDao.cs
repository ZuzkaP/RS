using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RS.Core;
using RS.Models;

namespace RS.DAO
{
    public class UsersDao : DatabaseObserver
    {
        private static int TRENER = 2;
        private static int POUZIVATEL = 3;

        public override void Update(MainDB database)
        {
            this.database = database;
        }

        public ICollection<Users> GetUsersByName(string sortOrder, string first_name)
        {
            var users = from s in database.Users
                        select s;

            if (!String.IsNullOrEmpty(first_name))
            {
                users = users.Where(s => s.first_name.Contains(first_name));
            }

            switch (sortOrder)
            {
                case "Meno:":
                    users = users.OrderByDescending(s => s.first_name);
                    break;
                case "Priezvisko:":
                    users = users.OrderByDescending(s => s.last_name);
                    break;
                default:
                    users = users.OrderBy(s => s.last_name);
                    break;
            }

            return users.ToList();
        }

        public Users GetUserByEmail(string email)
        {
            var user = from u in database.Users
                       where u.email.Equals(email)
                       select u;

            return user.ToList().ElementAt(0);
        }

        public ICollection<Users> GetUsersByFirstOrLastName(string name)
        {
            var users = from s in database.Users
                        select s;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(s => s.first_name.Contains(name) || s.last_name.Contains(name));
            }

            return users.ToList();
        }

        public int GetRoleIdByName(string role)
        {
            return database.Roles.Where(r => r.name.Equals(role)).ToList().ElementAt(0).roles_id;
        }

        public bool ExistsUser(string email)
        {
            return database.Users.Count(u => u.email == email) != 0;
        }

        public void SaveUser(Users U)
        {
            UsersRoles usersRoles = new UsersRoles();
            string hash = Helpers.SHA1.Encode(U.password);
            string hashConfirm = Helpers.SHA1.Encode(U.ConfirmPassword);
            U.ConfirmPassword = hashConfirm;
            U.password = hash;

            database.Users.Add(U);
            database.SaveChanges();
            usersRoles.roles_id = POUZIVATEL;

            usersRoles.user_id = U.user_id;
            database.UsersRoles.Add(usersRoles);
            database.SaveChanges();
        }

        public void SaveUsersRole(int userId, int roleId)
        {
            UsersRoles usersRoles = new UsersRoles();
            usersRoles.user_id = userId;
            usersRoles.roles_id = roleId;

            database.UsersRoles.Add(usersRoles);
        }

        public void EditUser(Users user, List<Roles> roles)
        {
            var u = GetUserByEmail(user.email);
            u.first_name = user.first_name;
            u.last_name = user.last_name;
            u.ConfirmPassword = u.password;
            u.phone_number = user.phone_number;

            List<UsersRoles> deletion = new List<UsersRoles>();
            foreach(UsersRoles usersRoles in u.UsersRoles)
            {
                if(!ContainsRole(usersRoles, roles))
                {
                    deletion.Add(usersRoles);
                }
            }

            foreach(UsersRoles ur in deletion)
            {
                u.UsersRoles.Remove(ur);
            }

            foreach(Roles role in roles)
            {
                if(!ContainsRole(role, u.UsersRoles))
                {
                    SaveUsersRole(u.user_id, GetRoleIdByName(role.name));
                }
            }
        }

        private bool ContainsRole(UsersRoles usersRoles, List<Roles> roles)
        {
            foreach(Roles role in roles)
            {
                if(role.name.Equals(usersRoles.Roles.name))
                {
                    return true;
                }
            }

            return false;
        }

        private bool ContainsRole(Roles role, ICollection<UsersRoles> usersRoles)
        {
            foreach(UsersRoles ur in usersRoles)
            {
                if(ur.Roles.name.Equals(role.name))
                {
                    return true;
                }
            }

            return false;
        }
    }
}