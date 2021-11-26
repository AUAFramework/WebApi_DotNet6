using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.BaseModel.BaseValidationModels;
using AUA.ProjectName.Models.ViewModels.Accounting.RoleModels;
using AUA.ProjectName.Services.EntitiesService.Accounting.Contracts;
using AUA.ProjectName.ValidationServices.Accounting.RoleValidation.Contracts;
using AUA.ServiceInfrastructure.BaseServices;
using Microsoft.EntityFrameworkCore;

namespace AUA.ProjectName.ValidationServices.Accounting.RoleValidation.Services
{
    public class RoleInsertValidationService : InfraValidationService, IRoleInsertValidationService
    {
        private RoleInsertVm _roleInsertVm;
        private readonly IRoleService _roleService;


        public RoleInsertValidationService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<ValidationResultVm> ValidationAsync(RoleInsertVm areaInsertVm)
        {
            _roleInsertVm = areaInsertVm;

            await DoValidationAsync();

            return ValidationResultVm;
        }

        private async Task DoValidationAsync()
        {

            DefaultValidation();

            if (HasError) return;

            await ValidationTitleAsync();
        }

        private void DefaultValidation()
        {
            if (!Enum<EAppType>.IsExistValue(_roleInsertVm.AppTypeCode.GetId()))
                AddError("AppType", "Invalid appType code");

            if (string.IsNullOrWhiteSpace(_roleInsertVm.Title))
                AddError("Title", "Title is empty !");

        }

        private async Task ValidationTitleAsync()
        {
            var isExistsRoleTitle = await IsExistsRoleTitleAsync();

            if (isExistsRoleTitle)
                AddError("Title", "Title is exists");

        }

        private async Task<bool> IsExistsRoleTitleAsync()
        {
            return await _roleService
                           .GetAll()
                           .AsNoTracking()
                           .AnyAsync(p => p.Title == _roleInsertVm.Title);
                          
        }


    }
}
