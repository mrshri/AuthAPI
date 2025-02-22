﻿using AuthApi.Models.DTO;

namespace AuthApi.Service.Iservice
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO  );
        Task<bool> AssignRole(string email, string roleName);
    }
}
