using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BadWebServer.Models;

namespace BadWebServer
{
public class XmlUserManager
{
    private readonly string filename = "users.xml";


    public OperationResult Create(Models.User user)
    {
        bool fileExists = System.IO.File.Exists(filename);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
        List<Models.User> _users;
        using (var stream = fileExists ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
        {

            _users = fileExists ? (List<Models.User>)serializer.Deserialize(stream) : new List<Models.User>();


            if (_users.Any(x => x.Username == user.Username))
            {
                return new OperationResult { Succeeded = false, Errors = new string[] { "Username is already taken" } };
            }
            else
            {
                _users.Add(new Models.User { Username = user.Username });
                if (fileExists)
                {
                    stream.Position = 0;
                }
                serializer.Serialize(stream, _users);
                return new OperationResult { Succeeded = true };
            }
        }
    }

    public OperationResult AddPassword(Models.User user, string password)
    {
        bool fileExists = System.IO.File.Exists(filename);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
        List<Models.User> _users;
        using (var stream = fileExists ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
        {

            _users = fileExists ? (List<Models.User>)serializer.Deserialize(stream) : new List<Models.User>();

            var existingUser = _users.FirstOrDefault(x => x.Username == user.Username);

            if (existingUser == null)
            {
                return new OperationResult { Succeeded = false, Errors = new string[] { "User does not exist" } };
            }
            else
            {
                var hasher = System.Security.Cryptography.SHA512.Create();
                existingUser.Password = System.Text.Encoding.Unicode.GetString(hasher.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password)));

                if (fileExists)
                {
                    stream.Position = 0;
                }
                serializer.Serialize(stream, _users);
                return new OperationResult { Succeeded = true };
            }
        }
    }

    public void Delete(User model)
    {
        bool fileExists = System.IO.File.Exists(filename);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
        List<Models.User> _users;
        using (var stream = fileExists ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
        {
            _users = fileExists ? (List<Models.User>)serializer.Deserialize(stream) : new List<Models.User>();
            _users = _users.Where(x => x.Username != model.Username).ToList();
            if (fileExists)
            {
                stream.SetLength(0);
                stream.Position = 0;
            }
            serializer.Serialize(stream, _users);
        }
    }

    public Models.User FindByName(string name)
    {
        bool fileExists = System.IO.File.Exists(filename);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
        List<Models.User> _users;
        using (var stream = fileExists ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
        {

            _users = fileExists ? (List<Models.User>)serializer.Deserialize(stream) : new List<Models.User>();

            return _users.FirstOrDefault(x => x.Username == name);

        }
    }

    public bool CheckPassword(Models.User user, string password)
    {
        bool fileExists = System.IO.File.Exists(filename);
        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Models.User>));
        List<Models.User> _users;
        using (var stream = fileExists ? System.IO.File.Open(filename, System.IO.FileMode.Open) : System.IO.File.Create(filename))
        {

            _users = fileExists ? (List<Models.User>)serializer.Deserialize(stream) : new List<Models.User>();

            var existingUser = _users.FirstOrDefault(x => x.Username == user.Username);

            if (existingUser == null)
            {
                return false;
            }
            else
            {
                var hasher = System.Security.Cryptography.SHA512.Create();
                return existingUser.Password == System.Text.Encoding.Unicode.GetString(hasher.ComputeHash(System.Text.Encoding.Unicode.GetBytes(password)));
            }
        }
    }

}

public class OperationResult
{
    public bool Succeeded { get; set; }

    public string[] Errors { get; set; }
}
}
