﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);

      /*  Task SaveFileAsync(Stream mediaBinaryStream, string fileName);*/
        Task<string> SaveImageAsync(IFormFile image);

        Task DeleteFileAsync(string fileName);
    }
}
