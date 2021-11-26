using System.Text;
using AUA.ProjectName.Common.Consts;
using AUA.ProjectName.Common.Enums;
using AUA.ProjectName.Common.Extensions;
using AUA.ProjectName.Models.BaseModel.BaseViewModels;

namespace AUA.ProjectName.WebApi.Controllers
{
    public class BaseApiController : InfraApiController
    {

        protected void ValidationSearchVm(BaseSearchVm searchVm)
        {
            if (searchVm.PageSize <= 0)
                AddError("PageSize", " PageSize can not be less than zero or zero");

            if (searchVm.TotalSize < 0)
                AddError("TotalSize", " TotalSize can not be less than zero ");

            if (searchVm.PageNumber <= 0)
                AddError("PageNumber", " PageNumber can not be less than zero or zero");

            if (!Enum<EAppType>.IsExistValue(searchVm.AppTypeCode.GetId()))
                AddError("AppType", "Invalid AppType code");

        }

        protected void SetAuditInfo<T>(BaseVm<T> baseVm)
        {
            baseVm.CreatorUserId = UserId;
        }


        protected string CreateLogMessage(params string[] messages)
        {
            var logMessage = new StringBuilder();

            foreach (var message in messages)
                logMessage.Append(message + AppConsts.LogSplitter);

            return logMessage.ToString();
        }
    }
}
