//using LibraryApp.Application.Services;
//using LibraryApp.Application.User;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;

//namespace LibraryApp.Api.Controllers;

//[ApiController]
//[Route("[controller]")]
//public class AuthenficationController : Controller
//{
//    private readonly UserService _userService;

//    public AuthenficationController(UserService userService)
//    {
//        _userService = userService;
//    }

//    [HttpPost("/login")]
//    public async Task<IActionResult> Login(LoginUserRequest request)
//    {
//        var token = await _userService.Login(request.Email, request.Password);
//    }

//    [HttpPost("/register")]
//    public async Task<IActionResult> Register()
//    {
//        throw new NotImplementedException();
//    }

//    [HttpGet("/logout")]
//    public IActionResult Logout()
//    {
//        throw new NotImplementedException();
//    }

//    [HttpGet("/getRole")]
//    [Authorize]
//    public IActionResult GetRole()
//    {
//        throw new NotImplementedException();
//    }

//    [HttpPost("/refresh")]
//    public async Task<IActionResult> Refresh()
//    {
//        throw new NotImplementedException();
//    }
//}
