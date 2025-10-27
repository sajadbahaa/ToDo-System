using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.Services.Jwt
{

//    ليش استخدمنا واجهة(Interface)؟

//إنت سويت IJwtService لأنك تتبع مبدأ Separation of Concerns + Dependency Inversion Principle
//(من مبادئ SOLID).

//الهدف من الواجهة:

//تفصل العقد(Contract) عن التنفيذ(Implementation).

//أي كلاس ثاني بالنظام يعتمد على IJwtService، مو محتاج يعرف شنو التفاصيل الداخلية لكيفية توليد التوكن.

//تقدر تبدّل التنفيذ لاحقًا(مثلاً لو أردت تستخدم نوع آخر من التوكنات) بدون ما تغيّر باقي الكود.

//🔸 مثال عملي:
//اليوم عندك JwtService،
//بكرة ممكن تسوي GoogleJwtService أو AzureAdTokenService بدون تغيّر كود الـ Controller.
 
    
    public  interface IJwtServices
    {
        string GenerateTokenAsync(AppUser user, List<string> roles);
    }
}
