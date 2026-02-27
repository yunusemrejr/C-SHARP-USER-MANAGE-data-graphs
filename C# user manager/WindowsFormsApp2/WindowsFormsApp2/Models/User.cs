using System;

namespace WindowsFormsApp2.Models
{
    /// <summary>
    /// Represents a user entity in the system
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        public float NV { get; set; }
        public string Username { get; set; }
        public string UserSurname { get; set; }
        public string Category { get; set; }
        public string Mission { get; set; }

        public User()
        {
            Username = string.Empty;
            UserSurname = string.Empty;
            Category = string.Empty;
            Mission = string.Empty;
        }

        public User(int id, float nv, string username, string userSurname, string category, string mission)
        {
            ID = id;
            NV = nv;
            Username = username ?? string.Empty;
            UserSurname = userSurname ?? string.Empty;
            Category = category ?? string.Empty;
            Mission = mission ?? string.Empty;
        }

        public string FullName => $"{Username} {UserSurname}";

        public override string ToString()
        {
            return $"ID: {ID}, Name: {FullName}, Category: {Category}, Mission: {Mission}, NV: {NV}";
        }
    }
}
