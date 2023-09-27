using Divisima.BL.Repositories;
using Divisima.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Divisima.API.Controllers
{
    [ApiController, Route("/api/[controller]"), Authorize]
    public class HomeController : ControllerBase
    {
        IRepository<Brand> repoBrand;
        IRepository<Admin> repoAdmin;
        public HomeController(IRepository<Brand> _repoBrand, IRepository<Admin> _repoAdmin)
        {
            repoBrand = _repoBrand;
            repoAdmin = _repoAdmin;
        }

        [HttpGet]
        public IEnumerable<Brand> GetBrands()
        {
            return repoBrand.GetAll().OrderBy(x => x.Name);
        }

        [HttpGet("{id}")]
        public Brand GetBrands(int id)
        {
            return repoBrand.GetBy(x => x.ID == id);
        }

        /// <summary>
        /// Add metodu ile şu şekilde veri gönderebilirsiniz: {"name": "Marka Adı"}
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public string Add (Brand model)
        {
            try
            {
                repoBrand.Add(model);
                return model.Name + " markası başarıyla eklendi";
            }
            catch (Exception ex)
            {
                return "Bir sorun oluştu, açıklaması: " + ex.Message;
            }
        }


        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                Brand brand = repoBrand.GetBy(x => x.ID == id);
                repoBrand.Delete(brand);
                return brand.Name + " isimli marka başarıyla silindi";
            }
            catch (Exception ex)
            {
                return "silme işleminde bir sorun oluştu, açıklama: " + ex.Message;
            }
        }

        [HttpPut]
        public string Update(Brand model)
        {
            try
            {
                repoBrand.Update(model);
                return model.Name + " isimli marka başarıyla güncellendi";
            }
            catch (Exception ex)
            {
                return "Güncelleme işleminde bir sorun oluştu, açıklama: " + ex.Message;
            }
        }

        [AllowAnonymous, Route("/api/login"), HttpGet]
        public string Login(string username, string password)
        {
            string md5Password = GetMD5(password);
            Admin admin = repoAdmin.GetBy(x => x.UserName == username && x.Password == md5Password);
            if (admin != null) // bir kayıt dönüyorsa
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.PrimarySid, admin.ID.ToString()),
                    new Claim(ClaimTypes.Name, admin.NameSurname)
                };

                string singinKey = "BubenimOzelSignInKey";
                SymmetricSecurityKey symmetricSecurityKey = new(Encoding.UTF8.GetBytes(singinKey));
                SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken jwtSecurityToken = new(
                    issuer: "http://localhost:5153", // token sağlayıcı url --- API
                    audience: "hepsiburada", // kimliği kullancak olan firma, uygulama adı
                    claims: claims,
                    expires: DateTime.Now.AddDays(10), // token geçerlilik süresi
                    signingCredentials: signingCredentials
                    );

                string jwToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                return jwToken;
            }
            else return "Başarısız...";
        }

        public static string GetMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }


    }
}
