using System.Collections.Generic;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;
using AUA.ProjectName.Models.EntitiesDto.Accounting;
using AUA.ProjectName.Models.GeneralModels.AccessTokenModels;

namespace AUA.ProjectName.Models.ViewModels.Accounting.UserRoleAccessModels
{
    public class UserRoleAccessIndexVm : BaseVm<int>
    {
        public RoleDto RoleDto { get; set; }

        public IEnumerable<UserRoleAccessVm> UserRoleAccessVms { get; set; }


    }
}
