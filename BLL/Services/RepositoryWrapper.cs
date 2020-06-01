using BLL.Interfaces;
using DAL.Interfaces.Repositories;
using DAL.Services;
using Entities;

namespace BLL
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IBrandRepository _brand;
        private IClothesCategoryRepository _clothesCategory;
        private IClothesRepository _clothes;
        private IClothesSizeRepository _clothesSize;
        private IGenderRepository _gender;
        private IReminderRepository _reminder;
        private IRoleRepository _role;
        private ISizeRepository _size;
        private IUserRepository _user;
        private IUserRoleRepository _userRole;

        public IBrandRepository Brand
        {
            get
            {
                if (_brand == null)
                {
                    _brand = new BrandRepository(_repoContext);
                }

                return _brand;
            }
        }
        public IClothesCategoryRepository ClothesCategory
        {
            get
            {
                if (_clothesCategory == null)
                {
                    _clothesCategory = new ClothesCategoryRepository(_repoContext);
                }

                return _clothesCategory;
            }
        }
        public IClothesRepository Clothes
        {
            get
            {
                if (_clothes == null)
                {
                    _clothes = new ClothesRepository(_repoContext);
                }

                return _clothes;
            }
        }
        public IClothesSizeRepository ClothesSize
        {
            get
            {
                if (_clothesSize == null)
                {
                    _clothesSize = new ClothesSizeRepository(_repoContext);
                }

                return _clothesSize;
            }
        }
        public IGenderRepository Gender
        {
            get
            {
                if (_gender == null)
                {
                    _gender = new GenderRepository(_repoContext);
                }

                return _gender;
            }
        }
        public IReminderRepository Reminder
        {
            get
            {
                if (_reminder == null)
                {
                    _reminder = new ReminderRepository(_repoContext);
                }

                return _reminder;
            }
        }
        public IRoleRepository Role
        {
            get
            {
                if (_role == null)
                {
                    _role = new RoleRepository(_repoContext);
                }

                return _role;
            }
        }
        public ISizeRepository Size
        {
            get
            {
                if (_size == null)
                {
                    _size = new SizeRepository(_repoContext);
                }

                return _size;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }
        public IUserRoleRepository UserRole
        {
            get
            {
                if (_userRole == null)
                {
                    _userRole = new UserRoleRepository(_repoContext);
                }

                return _userRole;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
