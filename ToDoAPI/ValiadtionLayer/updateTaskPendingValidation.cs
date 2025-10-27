using Dtos;
using FluentValidation;
using System;

namespace ValidationLayer
{
    public class updateTaskPendingValidation : AbstractValidator<updatePendingTaskDto>
    {
        public updateTaskPendingValidation()
        {
            // رقم المهمة مطلوب ولازم يكون أكبر من صفر
            RuleFor(x => x.taskID)
                .GreaterThan(0).WithMessage("رقم المهمة غير صالح.");

            // العنوان مطلوب وطوله بين 3 و100 حرف
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("العنوان مطلوب.")
                .MinimumLength(3).WithMessage("العنوان يجب أن يحتوي على 3 أحرف على الأقل.")
                .MaximumLength(100).WithMessage("العنوان لا يمكن أن يتجاوز 100 حرف.");

            // الوصف اختياري لكن لا يزيد عن 500 حرف
            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("الوصف لا يمكن أن يتجاوز 500 حرف.");

            // تاريخ الاستحقاق لازم يكون بعد الآن (لأنها مهمة لم تبدأ بعد)
            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("تاريخ الاستحقاق يجب أن يكون بعد اليوم.");
        }
    }
}
