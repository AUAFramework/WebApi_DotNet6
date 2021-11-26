using AUA.ProjectName.Common.Extensions;

namespace AUA.ProjectName.Models.BaseModel.BaseDto
{
    public class BaseReportEntityDto
    {

        public DateTime RegistrationDate { get; set; }


        public string RegDatePersian
        {
            get => RegistrationDate.ToPersianDate();
            set => RegistrationDate = value.ToDateTime();
        }

    }



}
