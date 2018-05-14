using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeetPhotos.Core.Models;

namespace LeetPhotos.Core.Services
{
    public interface IPhotoService
    {
        Task<List<Photo>> GetPhotosAsync();
    }
}
