using Dtos;
using FluentValidation;

namespace ValiadtionLayer
{
    public class updateTaskProgressValidation : AbstractValidator<updateProgressTaskDto>
    {
        public updateTaskProgressValidation() 
        {
            // رقم المهمة مطلوب ولازم يكون أكبر من صفر
            RuleFor(x => x.taskID)
                .GreaterThan(0).WithMessage("رقم المهمة غير صالح.");

            // الوصف اختياري لكن لا يزيد عن 500 حرف
            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("الوصف لا يمكن أن يتجاوز 500 حرف.");

        }

    }
}
