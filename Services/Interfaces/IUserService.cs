﻿using DiplomaAPI.Models.Users;

namespace DiplomaAPI.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        void Register(RegisterRequest model, string origin);
        //void VerifyEmail(string token);
        //void ForgotPassword(ForgotPasswordRequest model, string origin);
        //void ValidateResetToken(ValidateResetTokenRequest model);
        //void ResetPassword(ResetPasswordRequest model);
        IEnumerable<AccountResponse> GetAll();
        AccountResponse GetById(int id);
        AccountResponse Create(CreateRequest model);
        AccountResponse Update(int id, UpdateRequest model);
        AccountResponse UpdateProgress(int id, ProgressRequest model);
        void Delete(int id);
        void UploadProfiles(List<UploadProfilesRequest> model);
    }
}
