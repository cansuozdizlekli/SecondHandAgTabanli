using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecondHandMVC.Models;
using MySql.Data.MySqlClient;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SecondHandMVC.Models;
using Microsoft.Extensions.Logging;
using System;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Metadata;

namespace SecondHandMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
    {
        _logger = logger;
        this._hostEnvironment = hostEnvironment;
        _context = context;
    }

    public IActionResult Index()
    {
        
        return View();
    }

    [AllowAnonymous]
    public IActionResult Login()
    {

        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public async Task<IActionResult> MyPurchases()
    {
        return View(await _context.Advertisement.ToListAsync());
    }

    public async Task<IActionResult> MyAdvertisements()
    {
        return View(await _context.Advertisement.ToListAsync());
    }

    public async Task<IActionResult> Advertisements(int id)
    {
        return View(await _context.Advertisement.ToListAsync());
    }

    public IActionResult AddToCard([FromBody] int id)
    {
        string token = HttpContext.Request.Cookies["tokenString"];
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var uniqueNameClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
        if (uniqueNameClaim != null)
        {
            string userId = uniqueNameClaim.Value;
            Console.WriteLine("User ID: " + userId);
            try
            {
                Console.WriteLine("AddToCard içindeyim. Id: " + id);

                using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=root;Password=Cansu12.;Database=SecondHand"))
                {
                    con.Open();

                    // Advertisement'ın BuyerUserID'ini güncelle
                    MySqlCommand updateCmd = new MySqlCommand("UPDATE Advertisement SET BuyerUserID = @BuyerUserID WHERE ID = @ID", con);
                    updateCmd.Parameters.AddWithValue("@BuyerUserID", userId); // Yeni BuyerUserID değeri
                    updateCmd.Parameters.AddWithValue("@ID", id); // Güncellenmek istenen Advertisement'ın ID'si
                    updateCmd.ExecuteNonQuery();
                }

                return Json(new { success = true, message = "Product added to cart successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error adding product to cart." });
            }
        }

        // Eğer uniqueNameClaim null ise buraya düşer, burada bir şey döndürmek gerekirse ekleyebilirsiniz.
        return Json(new { success = false, message = "User ID not found." });
    }

    public IActionResult DeleteItem([FromBody] int id)
    {
        string token = HttpContext.Request.Cookies["tokenString"];
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var uniqueNameClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");
        if (uniqueNameClaim != null)
        {
            string userId = uniqueNameClaim.Value;
            Console.WriteLine("User ID: " + userId);
            try
            {
                Console.WriteLine("delete item içindeyim. Id: " + id);

                using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=root;Password=Cansu12.;Database=SecondHand"))
                {
                    con.Open();
                    MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM Advertisement WHERE ID = @ID", con);
                    deleteCmd.Parameters.AddWithValue("@ID", id);
                    deleteCmd.ExecuteNonQuery();
                    
                }

                return Json(new { success = true, message = "Product deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error delete product." });
            }
        }

        // Eğer uniqueNameClaim null ise buraya düşer, burada bir şey döndürmek gerekirse ekleyebilirsiniz.
        return Json(new { success = false, message = "User ID not found." });
    }


    public IActionResult AddAdvertisement()
    {
        string token = HttpContext.Request.Cookies["tokenString"];
        Console.WriteLine("token geliyo mu " + token);
        if (token != null)
        {
            return View();
        }
        else
        {
            // Token doesn't exist, redirect to the login page
            return Redirect("/Home/Login?tokenString=" + token);
        }
    }



    [HttpPost]
    public IActionResult AddAdvertisementToMysql()
    {
        
        string title = HttpContext.Request.Form["title"];
        string description = HttpContext.Request.Form["description"];
        string tags = HttpContext.Request.Form["tags"];
        string priceString = HttpContext.Request.Form["price"];
        int price = 0;
        
        int.TryParse(priceString, out price);

        string token = HttpContext.Request.Cookies["tokenString"];
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        var uniqueNameClaim = jsonToken.Claims.FirstOrDefault(claim => claim.Type == "unique_name");

        if (uniqueNameClaim != null)
        {
            string userId = uniqueNameClaim.Value;
            Console.WriteLine("User ID: " + userId);
            var files = HttpContext.Request.Form.Files;

            using (MemoryStream ms = new MemoryStream())
            {
                files[0].CopyTo(ms);
                byte[] imageBytes = ms.ToArray();

                string base64ImageRepresentation = Convert.ToBase64String(imageBytes);

                //Console.WriteLine("Base64 Temsil: " + base64ImageRepresentation);

                using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=root;Password=Cansu12.;Database=SecondHand"))
                {

                    try
                    {
                        con.Open();

                        MySqlCommand cmd = new MySqlCommand("INSERT INTO Advertisement (Title, Description, Tag, Price, Image, BuyerUserID, SellerUserID) VALUES (@Title, @Description, @Tag, @Price, @Image, @BuyerUserID, @SellerUserID)", con);
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Description", description);
                        cmd.Parameters.AddWithValue("@Tag", tags);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@Image", base64ImageRepresentation);
                        cmd.Parameters.AddWithValue("@BuyerUserID", -1);
                        cmd.Parameters.AddWithValue("@SellerUserID", userId);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("hata alıyom:" + ex.Message);
                    }

                }
            }
        }
        else
        {
            Console.WriteLine("unique_name claim bulunamadı");
        }

        return Redirect("/Home/Advertisements?tokenString=" + token);
    }



    public ActionResult AdvertisementDetail(int id)
    {
        // id parametresini kullanarak ilan detayını veritabanından çekin veya modeli oluşturun
        Advertisement advertisementDetail = GetAdvertisementDetailById(id);

        return View(advertisementDetail); // AdvertisementDetail.cshtml'e modeli göndererek sayfayı render edin
    }

    private Advertisement GetAdvertisementDetailById(int id)
    {
        try
        {
            using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=root;Password=Cansu12.;Database=SecondHand"))
            {
                con.Open();

                MySqlCommand selectCmd = new MySqlCommand("SELECT * FROM Advertisement WHERE ID = @ID", con);
                selectCmd.Parameters.AddWithValue("@ID", id);

                using (MySqlDataReader reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Advertisement advertisementDetail = new Advertisement
                        {
                            // Advertisement model sınıfına uygun şekilde alanları doldurun
                            Id = reader.GetInt32("Id"),
                            Title = reader.GetString("Title"),
                            Description = reader.GetString("Description"),
                            Image = reader.GetString("Image"),
                            Tag = reader.GetString("Description"),
                            Price = reader.GetInt32("Price"),
                            BuyerUserID = reader.GetInt32("BuyerUserId"),
                            SellerUserID = reader.GetInt32("SellerUserId")
                            
                        };     
                        return advertisementDetail;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error getting Advertisement detail: " + ex.Message);
            return null;
        }
    }


    [HttpPost]
    public IActionResult RegisterUser()
    {
        string username = HttpContext.Request.Form["username"];
        string email = HttpContext.Request.Form["email"];
        string password = HttpContext.Request.Form["password"];

        string hashedPassword = PasswordHasher.HashPassword(password);
        using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=root;Password=Cansu12.;Database=SecondHand"))
        {

            try
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Users (Name, Email, Password) VALUES (@Name, @Email, @Password)", con);
                cmd.Parameters.AddWithValue("Name", username);
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Parameters.AddWithValue("Password", hashedPassword);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("hata alıyom:", ex.Message);
            }

        }

        return Redirect("/Home/Login");
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult LoginUser()
    {
        string enteredEmail = HttpContext.Request.Form["email"];
        string enteredPassword = HttpContext.Request.Form["password"];
        string storedHashedPassword;
        int userId;
  
        using (MySqlConnection con = new MySqlConnection("Server=localhost;Port=3306;User ID=root;Password=Cansu12.;Database=SecondHand"))
        {
            con.Open();
            MySqlCommand selectCmd = new MySqlCommand("SELECT UserID, Password FROM Users WHERE Email = @Email", con);
            selectCmd.Parameters.AddWithValue("@Email", enteredEmail);

           
            using (var reader = selectCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    userId = reader.GetInt32("UserID");
                    storedHashedPassword = reader.GetString("Password");
                }
                else
                {
                    ViewBag.Message = "Invalid username or password!";
                    return View("Login", ViewBag);
                }
            }
        }

        bool isPasswordValid = PasswordHasher.VerifyPassword(enteredPassword, storedHashedPassword);
        Console.WriteLine("isPasswordValid şu " + isPasswordValid);
    

        if (isPasswordValid)
        {
            var token = GenerateJwtToken(userId);
            ViewBag.Token = token;
            return Redirect("/Home/Advertisements?tokenString=" + token);
        }
        else
        {
            ViewBag.Message = "Invalid username or password!";
            return View("Login", ViewBag);
        }

        
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private string GenerateJwtToken(int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("CansuKey12345678901234567890"); // Bu değer JWT'nin imzalanmasında kullanılacak gizli anahtar ile aynı olmalıdır.

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
            new Claim(ClaimTypes.Name, userId.ToString())
                // İhtiyaç duyulan diğer iddiaları (claims) ekleyebilirsiniz
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Token süresi (1 saat örneği)
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

