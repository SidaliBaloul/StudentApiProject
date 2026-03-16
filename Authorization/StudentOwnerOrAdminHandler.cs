using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace StudentApi.Authorization
{
    public class StudentOwnerOrAdminHandler : AuthorizationHandler<StudentOwnerOrAdminRequirments,int>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, StudentOwnerOrAdminRequirments requirement, int studentID)
        {
            if(context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(int.TryParse(userId , out int authenticatedstudentId) && authenticatedstudentId == studentID)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
