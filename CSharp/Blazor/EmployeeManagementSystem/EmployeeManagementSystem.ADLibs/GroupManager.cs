using EmployeeManagementSystem.ADLibs.Interfaces;
using EmployeeManagementSystem.ADLibs.Interfaces.Models;
using System;
using System.DirectoryServices.AccountManagement;

namespace EmployeeManagementSystem.ADLibs
{
    public class GroupManager : IGroupManager, IDisposable
    {
        private readonly string _username;
        private readonly string _password;
        private readonly string _domain;
        private readonly string _groupPath;
        private readonly string _userPath;
        private PrincipalContext _ctx;

        public GroupManager(string username, string password, string domain, string groupPath, string userPath)
        {
            _username = username;
            _password = password;
            _domain = domain;
            _groupPath = groupPath;
            _userPath = userPath;
            Initialize();
        }
        public void Create(string name, string description)
        {
            using var group = new GroupPrincipal(_ctx, name)
            {
                Description = description,
                IsSecurityGroup = true,
                GroupScope = GroupScope.Global
            };
            group.Save();
        }

        public bool Delete(string name)
        {
            using var group = FindGroup(name);
            if (group == null)
                return false;
            group.Delete();
            return true;
        }

        public bool AddToGroup(string userName, string groupName)
        {
            using var user = FindUser(userName);
            if (user == null)
                return false;
            using var group = FindGroup(groupName);
            if (group == null)
                return false;
            group.Members.Add(user);
            group.Save();
            return true;
        }

        public bool RemoveFromGroup(string userName, string groupName)
        {
            using var user = FindUser(userName);
            if (user == null)
                return false;
            using var group = FindGroup(groupName);
            if (group == null)
                return false;
            group.Members.Remove(user);
            group.Save();
            return true;
        }

        public ADGroup GetDetails(string name)
        {
            using var group = FindGroup(name);
            if (group == null)
                return null;
            return new ADGroup
            {
                Sid = group.Sid.ToString(),
                Name = group.Name,
                Descripion = group.Description,
                IsSecurityGroup = group.IsSecurityGroup,
                GroupScope = group.GroupScope.GetValueOrDefault().ToString(),
            };
        }

        private void Initialize()
            => _ctx = new PrincipalContext(ContextType.Domain, _domain, _groupPath, _username, _password);
        private GroupPrincipal FindGroup(string searchValue)
            => GroupPrincipal.FindByIdentity(_ctx, IdentityType.Name, searchValue);
        private UserPrincipal FindUser(string searchValue)
            => UserPrincipal.FindByIdentity(
                new PrincipalContext(ContextType.Domain, _domain, _userPath, _username, _password), 
                IdentityType.Name, searchValue); 

        public void Dispose()
        {
            if (_ctx != null)
                _ctx.Dispose();
        }
    }
}
