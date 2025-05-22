// TienThoBookStore.WebApp/ViewComponents/CartSummaryViewComponent.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Repositories.Interfaces;

public class CartSummaryViewComponent : ViewComponent
{
    private readonly IOrderRepository _orderRepo;
    private readonly UserManager<AppUser> _userManager;
    public CartSummaryViewComponent(IOrderRepository orderRepo, UserManager<AppUser> userManager)
    {
        _orderRepo = orderRepo;
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        int count = 0;

        // ViewComponent nên dùng UserClaimsPrincipal
        if (UserClaimsPrincipal.Identity?.IsAuthenticated == true &&
            !UserClaimsPrincipal.IsInRole("Admin"))
        {
            var user = await _userManager.GetUserAsync(UserClaimsPrincipal);
            if (user is not null)
            {
                // Phương thức repo trả về Task<int> ⇒ cần await
                count = await _orderRepo.GetCartItemCountAsync(user.Id);
            }
        }

        return Content(count.ToString());  // in badge
    }
}
