using Dtos;
using FluentValidation;
using System;

namespace ValidationLayer
{
    public class AddTaskValidation : AbstractValidator<addTaskDtos>
    {
        public AddTaskValidation()
        {
            // العنوان مطلوب وطوله بين 3 و 100 حرف
            RuleFor(x => x.title)
                .NotEmpty().WithMessage("العنوان مطلوب.")
                .MinimumLength(3).WithMessage("العنوان يجب أن يحتوي على 3 أحرف على الأقل.")
                .MaximumLength(100).WithMessage("العنوان لا يمكن أن يتجاوز 100 حرف.");

            // الوصف اختياري لكن إذا انكتب يكون بطول معقول
            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("الوصف لا يمكن أن يتجاوز 500 حرف.");

            // تاريخ الإنشاء لا يمكن أن يكون في المستقبل
            RuleFor(x => x.createdAt)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("تاريخ الإنشاء لا يمكن أن يكون في المستقبل.");

            // تاريخ الاستحقاق (DueDate) لازم يكون بعد تاريخ الإنشاء
            RuleFor(x => x.DueDate)
                .GreaterThan(x => x.createdAt)
                .WithMessage("تاريخ الاستحقاق يجب أن يكون بعد تاريخ الإنشاء.");

            // userID يكون موجود (لكن تم تجاهله بالـ Json) لذلك نتحقق منه بالداخل
            //RuleFor(x => x.userID)
            //    .NotEmpty().WithMessage("المستخدم المرتبط بالمهمة مطلوب.");
        
        }
    }
}
