using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoService.Web.Models;
using AutoService.Web.ViewModels;  // ✅ Добавили!

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Login() => View();
    public IActionResult Register() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded) return RedirectToAction("Index", "Requests");

        ModelState.AddModelError("", "Ошибка входа");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
    {
        Console.WriteLine(model);
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Requests");
        }

        ModelState.AddModelError("", "Ошибка регистрации");
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Requests");
    }
}
