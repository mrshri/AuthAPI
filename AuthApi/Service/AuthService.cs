using AuthApi.DATA;
using AuthApi.Models;
using AuthApi.Models.DTO;
using AuthApi.Service.Iservice;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace AuthApi.Service
{
    public class AuthService : IAuthService
    {
        readonly private AuthDbContext _context;
        readonly private UserManager<ApplicationUser> _userManger;
        readonly private RoleManager<IdentityRole> _roleManager;
        readonly private IJWTTokenGenerator _jWTTokenGenerator;
        public AuthService(AuthDbContext authDbContext, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IJWTTokenGenerator jWTTokenGenerator)
        {
            _context = authDbContext;
            _userManger = userManager;
            _roleManager = roleManager;
            _jWTTokenGenerator = jWTTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var User = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());
            if (User != null) { 
              if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
               await  _userManger.AddToRoleAsync(User, roleName);
                return true;

            }
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var User = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManger.CheckPasswordAsync(User, loginRequestDTO.Password);
            if (User == null || isValid == false)
            {
                return new LoginResponseDTO() { User = null, Token = "" };
            }
            //if user is valid, generate token
            var token  = _jWTTokenGenerator.GenerateToken(User);

            UserDTO userDTO = new UserDTO()
            {
                ID = User.Id,
                Email = User.Email,
                Name = User.Name,
                PhoneNumber = User.PhoneNumber
            };

            return new LoginResponseDTO() { User = userDTO, Token = token };
        }


        public async Task<string> Register(RegistrationRequestDTO registrationRequestDTO)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = registrationRequestDTO.Email,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.PhoneNumber

            };
            try
            {
                var result = await _userManger.CreateAsync(applicationUser, registrationRequestDTO.Password);
                if (result.Succeeded) { 
                var userToReturn = _context.ApplicationUsers.First(u => u.UserName == registrationRequestDTO.Email);
                    UserDTO userDTO = new UserDTO()
                    {
                        ID = userToReturn.Id,
                        Email = userToReturn.Email,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return  result.Errors.First().Description;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return "Error Encountered";
        }

    }
}
