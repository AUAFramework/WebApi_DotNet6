using AUA.ProjectName.Models.DataModels.LoginDataModels;
using AUA.ProjectName.Models.GeneralModels.LoginModels;


namespace AUA.ProjectName.Services.GeneralService.Login.Contracts
{
    public interface ILoginService 
    {
        Task<LoginDm> LoginAsync(LoginVm loginVm);

    }
}
