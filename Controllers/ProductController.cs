using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Application_test_repo.Repos;
using Application_test_repo.Helper;
using System.Globalization;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.EntityFrameworkCore;

namespace Application_test_repo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly Test_DBContext context;
        public ProductController(IWebHostEnvironment environment, Test_DBContext context)
        {
            _environment = environment;
            this.context = context;
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile formFile, string productCode)
        {
            APIResponse response = new APIResponse();
            try
            {
                string FilePath = GetFilepath(productCode);
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }

                string imagepath = FilePath + "\\" + productCode + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }

                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return Ok(response);
        }


        [HttpPut("MultiUploadImage")]
        public async Task<IActionResult> MultiUploadImage(IFormFileCollection filecollection, string productCode)
        {
            APIResponse response = new APIResponse();
            int passcont = 0;
            int errorcont = 0;
            try
            {
                string FilePath = GetFilepath(productCode);
                if (!System.IO.Directory.Exists(FilePath))
                {
                    System.IO.Directory.CreateDirectory(FilePath);
                }

                foreach (var file in filecollection)
                {
                    string imagepath = FilePath + "\\" + file.FileName;

                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }

                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        passcont++;

                    }
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }

            response.ResponseCode = 200;
            response.Result = passcont + " Files uploaded & " + errorcont + " files falied";
            return Ok(response);
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(string producecode)
        {
            string ImageUrl = string.Empty;
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilepath(producecode);
                string imagepath = Filepath + "\\" + producecode + ".png";

                if (System.IO.File.Exists(imagepath))
                {
                    ImageUrl = hosturl + "/Upload/product/" + producecode + "/" + producecode + ".png";

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(ImageUrl);
        }

        [HttpGet("GetMultiImage")]
        public async Task<IActionResult> GetMultiImage(string producecode)
        {
            List<string> Imageurl = new List<string>();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilepath(producecode);

                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();

                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string filename = fileInfo.Name;
                        string imagepath = Filepath + "\\" + filename;

                        if (System.IO.File.Exists(imagepath))
                        {
                            string _Imageurl = hosturl + "/Upload/product/" + producecode + "/" + filename;
                            Imageurl.Add(_Imageurl);
                        }
                    }

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(Imageurl);
        }

        [HttpGet("download")]
        public async Task<IActionResult> download(string productcode)
        {
            try
            {
                string Filepath = GetFilepath(productcode);
                string imagepath = Filepath + "\\" + productcode + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    MemoryStream stream = new MemoryStream();
                    using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(stream);
                    }
                    stream.Position = 0;
                    return File(stream, "image/png", productcode + ".png");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("RemoveImage")]
        public async Task<IActionResult> RemoveImage(string productcode)
        {
            try
            {
                string Filepath = GetFilepath(productcode);
                string imagepath = Filepath + "\\" + productcode + ".png";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                    return Ok("pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpGet("MultiremoveImages")]
        public async Task<IActionResult> MultiremoveImages(string productcode)
        {
            try
            {
                string Filepath = GetFilepath(productcode);
                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        fileInfo.Delete();
                    }
                    return Ok("pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPut("DataBaseBMultiUploadImage")]
        public async Task<IActionResult> DataBaseBMultiUploadImage(IFormFileCollection filecollection, string productcode)
        {
            APIResponse response = new APIResponse();
            int passcount = 0; int errorcount = 0;
            try
            {
                foreach (var file in filecollection)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        this.context.TblProductimages.Add(new Repos.Models.TblProductimage()
                        {
                            Code = productcode,
                            Productimage = stream.ToArray()
                        });
                        await this.context.SaveChangesAsync();
                        passcount++;
                    }
                }


            }
            catch (Exception ex)
            {
                errorcount++;
                response.ErrorMessage = ex.Message;
            }
            response.ResponseCode = 200;
            response.Result = passcount + " Files uploaded &" + errorcount + " files failed";
            return Ok(response);
        }

        [HttpGet("GetDataBaseMultiImages")]
        public async Task<IActionResult> GetDataBaseMultiImages(string productcode)
        {
            List<string> Imageurl = new List<string>();

            try
            {
                var _productimage = this.context.TblProductimages.Where(item => item.Code == productcode).ToList();
                if (_productimage != null && _productimage.Count > 0)
                {
                    _productimage.ForEach(item =>
                    {
                        Imageurl.Add(Convert.ToBase64String(item.Productimage));
                    });
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {

            }
            return Ok(Imageurl);

        }

        [HttpGet("DataBasedownload")]
        public async Task<IActionResult> DataBasedownload(string productcode)
        {
            try
            {
                var _productimage = await this.context.TblProductimages.FirstOrDefaultAsync(item => item.Code == productcode);
                if (_productimage != null)
                {
                    return File(_productimage.Productimage, "image/png", productcode + ".png");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        [NonAction]
        private string GetFilepath(string productcode)
        {
            return this._environment.WebRootPath + "\\Upload\\product\\" + productcode;
        }

    }
}
