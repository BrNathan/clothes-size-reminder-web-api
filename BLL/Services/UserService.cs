using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserService: IUserService
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly IUserRepository _userRepository;

        public UserService(RepositoryContext repositoryContext, IUserRepository userRepository)
        {
            this._repositoryContext = repositoryContext;
            this._userRepository = userRepository;
        }

        public void Save()
        {
            this._repositoryContext.SaveChanges();
        }

        public void CreateUser(User user)
        {
            _userRepository.Create(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public void UpdateUser(User dbUser, User user)
        {
            _userRepository.UpdateUser(dbUser, user);
        }
    }
}
